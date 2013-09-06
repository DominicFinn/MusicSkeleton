using NAudio.Wave;

namespace Music.Core
{
    public abstract class WaveProvider : IWaveProvider
    {
        protected WaveProvider()
            : this(44100, 1)
        {
        }

        public WaveFormat WaveFormat { get; private set; }

        protected WaveProvider(int sampleRate, int channels)
        {
            SetWaveFormat(sampleRate, channels);
        }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            var waveBuffer = new WaveBuffer(buffer);
            var samplesRequired = count / 4;
            var samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }

        public abstract int Read(float[] buffer, int offset, int sampleCount);
    }
}