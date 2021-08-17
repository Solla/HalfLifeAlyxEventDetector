namespace HalfLifeLogger
{
    static class Force_Pattern
    {
        /// <summary>
        /// [ RightLeft, FrontRear, UpDown, Duration_MS ]
        /// </summary>
        public static readonly double[]
            StartGrab = new double[] { 0, 1, 0, 1000 },
            StopGrab = new double[] { 0, -2, 0, 95 },
            EnergygunEjectClip = new double[] { 0, 0, 0.5, 145 },
            ChamberedRound_1 = new double[] { 0, -0.5, 0, 95 },
            ChamberedRound_2 = new double[] { 0, 3, 0, 45 },
            ShotGun_Open_1 = new double[] { 0, 1.5, 0, 195 },
            ShotGun_Open_2 = new double[] { 0, 0, -3, 95 },
            ShotGun_Close_1 = new double[] { 0, 0, -1.5, 195 },
            ShotGun_Close_2 = new double[] { 0, -3, 0, 95 },
            Shotgun_Chambered = new double[] { 0, 1.5, 0, 95 },
            Rapidfire_Energygun_Fire_1 = new double[] { 0, -3, 0, 45 },
            Rapidfire_Energygun_Fire_2 = new double[] { 0, +3, 0, 45 },
            Shotgun_Fire = new double[] { 0, -3.5, +3.5, 95 },
            InsertBullet_Energygun_1 = new double[] { 0, 0, 0.5, 145 },
            InsertBullet_Energygun_2 = new double[] { 0, 0, 0.8, 45 },
            InsertBullet_Shotgun = new double[] { 0, -0.8, -1.6, 145 },
            InsertBullet_Rapidfire_1 = new double[] { 0.5, 0, 0, 145 },
            InsertBullet_Rapidfire_2 = new double[] { 1.5, 0, 0, 45 };
    }
}
