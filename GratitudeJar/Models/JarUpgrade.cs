using System;

namespace GratitudeJar.Models
{
    public class JarUpgrade
    {
        private JarTier _currentTier;
        private int _entryLimit;

        public JarTier CurrentTier
        {
            get { return _currentTier; }
        }

        public int EntryLimit
        {
            get { return _entryLimit; }
        }

        public JarUpgrade()
        {
            _currentTier = JarTier.Glass;
            _entryLimit = 30;
        }

        public JarTier applyUpgrade(int entryCount)
        {
            if (entryCount >= 100 && _currentTier != JarTier.Diamond)
            {
                _currentTier = JarTier.Diamond;
                _entryLimit = int.MaxValue;
                Console.WriteLine("🎉 Congratulations! Your jar is now DIAMOND tier!");
            }
            else if (entryCount >= 31 && _currentTier == JarTier.Glass)
            {
                _currentTier = JarTier.Gold;
                _entryLimit = 100;
                Console.WriteLine("✨ Amazing! Your jar upgraded to GOLD tier!");
            }

            return _currentTier;
        }

        public JarTier getCurrentTier()
        {
            return _currentTier;
        }

        public int getEntryLimit()
        {
            return _entryLimit;
        }
    }
}