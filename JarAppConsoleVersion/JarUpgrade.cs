public class JarUpgrade
{
    private JarTier currentTier;
    private int entryLimit;

    public JarUpgrade(JarTier tier, int limit) { currentTier = tier; entryLimit = limit; }
    public JarTier ApplyUpgrade(int entryCount)
    {
        if (entryCount > 100) return JarTier.Diamond;
        if (entryCount > 50) return JarTier.Gold;
        return JarTier.Glass;
    }
}
