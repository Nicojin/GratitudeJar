namespace GratitudeJar.Models
{
    public class JarUpgrade
    {
        public JarTier CurrentTier { get; private set; } = JarTier.Glass;
        public int EntryLimit { get; private set; } = 30;

        public JarTier ApplyUpgrade(int entryCount)
        {
            if (entryCount >= 100)
                CurrentTier = JarTier.Diamond;
            else if (entryCount >= 30)
                CurrentTier = JarTier.Gold;
            return CurrentTier;
        }
    }
}