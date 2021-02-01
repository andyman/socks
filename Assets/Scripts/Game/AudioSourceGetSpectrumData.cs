using UnityEngine;

[System.Serializable]
public class Band
{
	public int startSpectrum;
	public int endSpectrum;
	public float value = 0.0f;
	public float lerpRate = 10.0f;
	public string shaderVariable;
	public float preMultiplier = 2.0f;

	public float power = 4.0f;

	public float bias = 10.0f;

	public float threshold = 0.2f;
	public float postThresholdMultiplier = 1.0f;
	public float finalValue
	{
		get
		{
			return value < threshold ? 0.0f : value * postThresholdMultiplier;
		}
	}
}
public class AudioSourceGetSpectrumData : MonoBehaviour
{
	public int spectrumSize = 1024;
	public int interestingSpectrumStart = 0;
	public int interestingSpectrumFinish = 512;

	float[] spectrum;
	public Band[] bands;

	public int bandCount = 20;

	int shaderPropertyId;

	private void Start()
	{
		shaderPropertyId = Shader.PropertyToID("_BeatOutput");
		spectrum = new float[spectrumSize];

		//GenerateBands();
		for (int i = 0; i < bands.Length; i++)
		{
			bands[i].value = 0.0f;
		}
	}

	[ContextMenu("Generate Bands")]
	public void GenerateBands()
	{
		float spectrumSizeFloat = (float)spectrumSize;

		float interestingSpectrumSize = interestingSpectrumFinish - interestingSpectrumStart;

		float bandSize = interestingSpectrumSize / ((float)bandCount);

		bands = new Band[bandCount];
		for (int i = 0; i < bandCount; i++)
		{
			Band b = new Band();
			b.startSpectrum = interestingSpectrumStart + Mathf.FloorToInt(bandSize * ((float)i));
			b.endSpectrum = interestingSpectrumStart + Mathf.FloorToInt(bandSize * ((float)(i + 1))) - 1;
			bands[i] = b;
		}
	}

	void Update()
	{

		AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

		for (int i = 0; i < bands.Length; i++)
		{
			UpdateBand(bands[i]);
			Shader.SetGlobalFloat(bands[i].shaderVariable, bands[i].finalValue);
		}

	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		for (int i = 0; i < bands.Length; i++)
		{
			float height = bands[i].finalValue;

			Gizmos.DrawCube(Vector3.right * i * 0.5f, new Vector3(0.25f, height, 0.0f));
		}

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(bands.Length * 0.5f, 0.0f, 0.0f));
		for (int i = 0; i < 10; i++)
		{
			Gizmos.DrawLine(new Vector3(0.0f, i, 0.0f), new Vector3(bands.Length * 0.5f, i, 0.0f));

		}
	}
	void UpdateBand(Band band)
	{
		float newValue = 0.0f;

		for (int i = band.startSpectrum; i <= band.endSpectrum; i++)
		{
			newValue += Mathf.Pow(Mathf.Max(Mathf.Log(spectrum[i]) + band.bias, 0.0f) / band.bias * band.preMultiplier, band.power);
		}
		newValue /= (float)(band.endSpectrum - band.startSpectrum + 1);
		newValue *= 0.5f;
		band.value = Mathf.Lerp(band.value, newValue, Time.deltaTime * band.lerpRate);
	}
}