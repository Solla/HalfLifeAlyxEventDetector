using HalfLifeLogger;
using System;
using System.IO;
using System.Threading;
using static HalfLifeAlyxEventDetector.HalfLifeAlyxGameMonitor;

namespace HalfLifeAlyxEventDetector
{
    class Program
    {
        static HardwareControl HardwareCtrl;
        static string Get_COM_Port_Name()
        {
            if (!File.Exists("JetController_COM_Port.txt"))
            {
                Console.WriteLine("JetController_COM_Port.txt not found!");
                string COM_Name;
                while (true)
                {
                    Console.WriteLine("Please Enter your COM Port: ");
                    if (!(COM_Name = Console.ReadLine()).StartsWith("COM", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Incorrect COM Port Name. Should start with \"COM\"");
                        continue;
                    }
                    File.WriteAllText("JetController_COM_Port.txt", COM_Name.Replace(" ", "").Trim());
                    break;
                }
            }
            return File.ReadAllText("JetController_COM_Port.txt").Trim().Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }

        static void Main(string[] args)
        {
            HardwareCtrl = new HardwareControl(Get_COM_Port_Name(), 500000);

            if (!Environment.Is64BitProcess)
                throw new Exception("Please build this application with 64 bit");
            HalfLifeAlyx_Manager HLA_Manager = new HalfLifeAlyx_Manager();
            HalfLifeAlyx_Autoexec HLA_Autoexec =
                new HalfLifeAlyx_Autoexec().
                UnlimitedMagazineInBag(true).
                BottomlessMagazine(false);
            if (!HLA_Manager.RestartHalfLifeAlyx(HLA_Autoexec))
                throw new Exception("Cannot open Half-Life: Alyx!");
            HalfLifeAlyxGameMonitor GameMonitor = new HalfLifeAlyxGameMonitor();
            GameMonitor.RegisterEventHandler(EventCallbackHandler);
            GameMonitor
                .SetBuddha(true)
                .WaitPlayerReloaded() //Wait for menu
                .Map("a3_hotel_street")
                .WaitPlayerReloaded() //Wait for teleportation ended
                .SendCommandToHalfLifeAlyx("impulse 101")
                .SendCommandToHalfLifeAlyx("setpos 2529.315674 -647.824463 167.968781")
                .Give("item_hlvr_weapon_energygun")
                .Give("item_hlvr_weapon_shotgun")
                .Give("item_hlvr_weapon_rapidfire")
                .Give("item_hlvr_clip_energygun")
                .Give("item_hlvr_clip_shotgun_multiple")
                .Give("item_hlvr_clip_rapidfire")
                .SendCommandToHalfLifeAlyx("hlvr_addresources 10 10 10 10");
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
        static void ShootHapticFeedback(NowWeaponType WeaponType)
        {
            switch (WeaponType)
            {
                case NowWeaponType.Energygun:
                case NowWeaponType.Rapidfire:
                    HardwareCtrl.ApplyForce(Force_Pattern.Rapidfire_Energygun_Fire_1[0], Force_Pattern.Rapidfire_Energygun_Fire_1[1], Force_Pattern.Rapidfire_Energygun_Fire_1[2], (uint)Force_Pattern.Rapidfire_Energygun_Fire_1[3]);
                    Thread.Sleep((int)(Force_Pattern.Rapidfire_Energygun_Fire_1[3] + 5));
                    HardwareCtrl.CloseAllValve();
                    HardwareCtrl.ApplyForce(Force_Pattern.Rapidfire_Energygun_Fire_2[0], Force_Pattern.Rapidfire_Energygun_Fire_2[1], Force_Pattern.Rapidfire_Energygun_Fire_2[2], (uint)Force_Pattern.Rapidfire_Energygun_Fire_2[3]);
                    Thread.Sleep((int)(Force_Pattern.Rapidfire_Energygun_Fire_2[3] + 5));
                    HardwareCtrl.CloseAllValve();
                    HardwareCtrl.PredictAllAt(0.5);
                    break;
                case NowWeaponType.Shotgun:
                    HardwareCtrl.ApplyForce(Force_Pattern.Shotgun_Fire[0], Force_Pattern.Shotgun_Fire[1], Force_Pattern.Shotgun_Fire[2], (uint)Force_Pattern.Shotgun_Fire[3]);
                    Thread.Sleep((int)(Force_Pattern.Shotgun_Fire[3] + 5));
                    HardwareCtrl.CloseAllValve();
                    HardwareCtrl.PredictAllAt(0.5);
                    break;
            }
        }
        static void SwitchWeaponHapticFeedback(NowWeaponType WeaponType)
        {
            switch (WeaponType)
            {
                case NowWeaponType.Shotgun:
                    HardwareCtrl.SendPredictForce(Force_Pattern.Shotgun_Fire[0], Force_Pattern.Shotgun_Fire[1], Force_Pattern.Shotgun_Fire[2]);
                    break;
                case NowWeaponType.Rapidfire:
                case NowWeaponType.Energygun:
                    HardwareCtrl.SendPredictForce(Force_Pattern.Rapidfire_Energygun_Fire_1[0], Force_Pattern.Rapidfire_Energygun_Fire_1[1], Force_Pattern.Shotgun_Fire[2]);
                    HardwareCtrl.SendPredictForce(Force_Pattern.Rapidfire_Energygun_Fire_2[0], Force_Pattern.Rapidfire_Energygun_Fire_2[1], Force_Pattern.Shotgun_Fire[2]);
                    break;
            }
            return;
        }
        static void InsertBulletHapticFeedback(NowWeaponType WeaponType)
        {
            switch (WeaponType)
            {
                case NowWeaponType.Energygun:
                    HardwareCtrl.ApplyForce(Force_Pattern.InsertBullet_Energygun_1[0], Force_Pattern.InsertBullet_Energygun_1[1], Force_Pattern.InsertBullet_Energygun_1[2], (uint)Force_Pattern.InsertBullet_Energygun_1[3]);
                    Thread.Sleep((int)(Force_Pattern.InsertBullet_Energygun_1[3] + 5));
                    HardwareCtrl.ApplyForce(Force_Pattern.InsertBullet_Energygun_2[0], Force_Pattern.InsertBullet_Energygun_2[1], Force_Pattern.InsertBullet_Energygun_2[2], (uint)Force_Pattern.InsertBullet_Energygun_2[3]);
                    Thread.Sleep((int)(Force_Pattern.InsertBullet_Energygun_2[3] + 5));
                    HardwareCtrl.CloseAllValve();
                    HardwareCtrl.PredictAllAt(0.5);
                    break;
                case NowWeaponType.Shotgun:
                    HardwareCtrl.ApplyForce(Force_Pattern.InsertBullet_Shotgun[0], Force_Pattern.InsertBullet_Shotgun[1], Force_Pattern.InsertBullet_Shotgun[2], (uint)Force_Pattern.InsertBullet_Shotgun[3]);
                    Thread.Sleep((int)(Force_Pattern.InsertBullet_Shotgun[3] + 5));
                    HardwareCtrl.CloseAllValve();
                    HardwareCtrl.PredictAllAt(0.5);
                    break;
                case NowWeaponType.Rapidfire:
                    HardwareCtrl.ApplyForce(Force_Pattern.InsertBullet_Rapidfire_1[0], Force_Pattern.InsertBullet_Rapidfire_1[1], Force_Pattern.InsertBullet_Rapidfire_1[2], (uint)Force_Pattern.InsertBullet_Rapidfire_1[3]);
                    Thread.Sleep((int)(Force_Pattern.InsertBullet_Rapidfire_1[3] + 5));
                    HardwareCtrl.ApplyForce(Force_Pattern.InsertBullet_Rapidfire_2[0], Force_Pattern.InsertBullet_Rapidfire_2[1], Force_Pattern.InsertBullet_Rapidfire_2[2], (uint)Force_Pattern.InsertBullet_Rapidfire_2[3]);
                    Thread.Sleep((int)(Force_Pattern.InsertBullet_Rapidfire_2[3] + 5));
                    HardwareCtrl.CloseAllValve();
                    HardwareCtrl.PredictAllAt(0.5);
                    break;
            }
        }
        static void GloveStartGrab(bool RightHand)
        {
            if (!RightHand)
                return;
            HardwareCtrl.ApplyForce(Force_Pattern.StartGrab[0], Force_Pattern.StartGrab[1], Force_Pattern.StartGrab[2], (uint)Force_Pattern.StartGrab[3]);
        }
        static void GloveStopGrab(bool RightHand)
        {
            if (!RightHand)
                return;
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.ApplyForce(Force_Pattern.StopGrab[0], Force_Pattern.StopGrab[1], Force_Pattern.StopGrab[2], (uint)Force_Pattern.StopGrab[3]);
            Thread.Sleep((int)(Force_Pattern.StopGrab[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.PredictAllAt(0.5);

        }
        static void StartTeleportation()
        {
        }

        static void EnergygunEjectClip()
        {
            HardwareCtrl.ApplyForce(Force_Pattern.EnergygunEjectClip[0], Force_Pattern.EnergygunEjectClip[1], Force_Pattern.EnergygunEjectClip[2], (uint)Force_Pattern.EnergygunEjectClip[3]);
            Thread.Sleep((int)(Force_Pattern.EnergygunEjectClip[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.PredictAllAt(0.5);
        }

        static void EnergygunChamberedRound()
        {
            HardwareCtrl.ApplyForce(Force_Pattern.ChamberedRound_1[0], Force_Pattern.ChamberedRound_1[1], Force_Pattern.ChamberedRound_1[2], (uint)Force_Pattern.ChamberedRound_1[3]);
            Thread.Sleep((int)(Force_Pattern.ChamberedRound_1[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.ApplyForce(Force_Pattern.ChamberedRound_2[0], Force_Pattern.ChamberedRound_2[1], Force_Pattern.ChamberedRound_2[2], (uint)Force_Pattern.ChamberedRound_2[3]);
            Thread.Sleep((int)(Force_Pattern.ChamberedRound_2[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.PredictAllAt(0.5);
        }

        static void Shotgun_Open()
        {
            HardwareCtrl.ApplyForce(Force_Pattern.ShotGun_Open_1[0], Force_Pattern.ShotGun_Open_1[1], Force_Pattern.ShotGun_Open_1[2], (uint)Force_Pattern.ShotGun_Open_1[3]);
            Thread.Sleep((int)(Force_Pattern.ShotGun_Open_1[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.ApplyForce(Force_Pattern.ShotGun_Open_2[0], Force_Pattern.ShotGun_Open_2[1], Force_Pattern.ShotGun_Open_2[2], (uint)Force_Pattern.ShotGun_Open_2[3]);
            Thread.Sleep((int)(Force_Pattern.ShotGun_Open_2[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.PredictAllAt(0.5);
        }

        static void Shotgun_Close()
        {
            HardwareCtrl.ApplyForce(Force_Pattern.ShotGun_Close_1[0], Force_Pattern.ShotGun_Close_1[1], Force_Pattern.ShotGun_Close_1[2], (uint)Force_Pattern.ShotGun_Close_1[3]);
            Thread.Sleep((int)(Force_Pattern.ShotGun_Close_1[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.ApplyForce(Force_Pattern.ShotGun_Close_2[0], Force_Pattern.ShotGun_Close_2[1], Force_Pattern.ShotGun_Close_2[2], (uint)Force_Pattern.ShotGun_Close_2[3]);
            Thread.Sleep((int)(Force_Pattern.ShotGun_Close_2[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.PredictAllAt(0.5);
        }

        static void Shotgun_Chambered()
        {
            HardwareCtrl.ApplyForce(Force_Pattern.Shotgun_Chambered[0], Force_Pattern.Shotgun_Chambered[1], Force_Pattern.Shotgun_Chambered[2], (uint)Force_Pattern.Shotgun_Chambered[3]);
            Thread.Sleep((int)(Force_Pattern.Shotgun_Chambered[3] + 5));
            HardwareCtrl.CloseAllValve();
            HardwareCtrl.PredictAllAt(0.5);
        }

    }
}
