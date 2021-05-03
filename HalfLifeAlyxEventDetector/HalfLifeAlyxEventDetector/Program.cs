using System;
using System.Threading;
using static HalfLifeAlyxEventDetector.HalfLifeAlyxGameMonitor;

namespace HalfLifeAlyxEventDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Environment.Is64BitProcess)
                throw new Exception("Please build this application with 64 bit");
            HalfLifeAlyx_Manager HLA_Manager = new HalfLifeAlyx_Manager();
            HalfLifeAlyx_Autoexec HLA_Autoexec =
                new HalfLifeAlyx_Autoexec().
                UnlimitedMagazineInBag(true).
                GiveAllUnlockedWeapons(true);
            if (!HLA_Manager.RestartHalfLifeAlyx(HLA_Autoexec))
                throw new Exception("Cannot open Half-Life: Alyx!");
            HalfLifeAlyxGameMonitor GameMonitor = new HalfLifeAlyxGameMonitor();
            GameMonitor.RegisterEventHandler(EventCallbackHandler);
            GameMonitor
                .SetBuddha(true)
                .Map("a1_intro_world");
            Console.ReadLine();
        }
        static void EventCallbackHandler(NowWeaponType WeaponType, HapticEventType Haptic)
        {
            if (Haptic == HapticEventType.WeaponShoot)
                ShootHapticFeedback(WeaponType);
            else if (Haptic == HapticEventType.WeaponChange)
                SwitchWeaponHapticFeedback(WeaponType);
            else if (Haptic == HapticEventType.InsertBullet)
                InsertBulletHapticFeedback(WeaponType);
            else if (Haptic == HapticEventType.LeftHandGrabStart || Haptic == HapticEventType.RightHandGrabStart)
                GloveStartGrab(Haptic == HapticEventType.RightHandGrabStart);
            else if (Haptic == HapticEventType.RightHandGrabStop || Haptic == HapticEventType.RightHandPickupItem)
                GloveStopGrab(true);
            else if (Haptic == HapticEventType.LeftHandGrabStop || Haptic == HapticEventType.LeftHandPickupItem)
                GloveStopGrab(false);
            else if (Haptic == HapticEventType.PlayerTeleportStart)
                StartTeleportation();
            else if (WeaponType == NowWeaponType.Energygun && Haptic == HapticEventType.EnergygunEjectClip)
                EnergygunEjectClip();
            else if (WeaponType == NowWeaponType.Energygun && Haptic == HapticEventType.ChamberedRound)
                EnergygunChamberedRound();
            else if (WeaponType == NowWeaponType.Shotgun && Haptic == HapticEventType.Shotgun_Open)
                Shotgun_Open();
            else if (WeaponType == NowWeaponType.Shotgun && Haptic == HapticEventType.Shotgun_Close)
                Shotgun_Close();
            else if (WeaponType == NowWeaponType.Shotgun && Haptic == HapticEventType.Shotgun_Chambered)
                Shotgun_Chambered();
            Console.WriteLine($"Event {Enum.GetName(typeof(HapticEventType), Haptic)} with Weapon {Enum.GetName(typeof(NowWeaponType), WeaponType)}");
        }
        private static void ShootHapticFeedback(NowWeaponType WeaponType)
        {
            switch (WeaponType)
            {
                case NowWeaponType.Energygun:
                    break;
                case NowWeaponType.Rapidfire:
                    break;
                case NowWeaponType.Shotgun:
                    break;
            }
        }
        private static void SwitchWeaponHapticFeedback(NowWeaponType WeaponType)
        {
            switch (WeaponType)
            {
                case NowWeaponType.Shotgun:
                    break;
                case NowWeaponType.Rapidfire:
                    break;
                case NowWeaponType.Energygun:
                    break;
            }
            return;
        }
        private static void InsertBulletHapticFeedback(NowWeaponType WeaponType)
        {
            switch (WeaponType)
            {
                case NowWeaponType.Energygun:
                    break;
                case NowWeaponType.Shotgun:
                    break;
                case NowWeaponType.Rapidfire:
                    break;
            }
        }
        static void GloveStartGrab(bool RightHand)
        {
        }
        static void GloveStopGrab(bool RightHand)
        {
        }
        static void StartTeleportation()
        {
        }

        static void EnergygunEjectClip()
        {
        }

        static void EnergygunChamberedRound()
        {
        }

        static void Shotgun_Open()
        {
        }

        static void Shotgun_Close()
        {
        }

        static void Shotgun_Chambered()
        {
        }

    }
}
