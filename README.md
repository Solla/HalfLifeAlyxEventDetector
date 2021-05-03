# README

This program is capable of detecting game events in Half-Life: Alyx, such as shooting, teleportation, and grabing stuffs.

Haptic developers can use this program to integrate your haptic devices into Half-Life: Alyx.

The method has been utilized by JetController(CHI'21 Paper, CHI'21 Interactivity, SIGGRAPH'21 Demo).

# Caution


## The program is licensed under the GNU General Public License v2.0

When distributing derived works, the source code of the repository must be made available.

It means you should always make your project open-sourced if you want to clone and modify the repository.

### BibTeX for JetController CHI'21 Full Paper
```
(Publish after 2021/5/8)
```

### BibTeX for JetController SIGGRAPH'21 Demo
```
(Not Published Yet)
```

### BibTeX for the repository
```
@MISC {HalfLifeAlyxEventDetector,
    title   = "Half-Life: Alyx Event Detector",
    url     = "https://github.com/Solla/HalfLifeAlyxEventDetector",
    year =  "2021",
    author = "Solla"
}
```
## Citation for Acadamaic Usage

Due to the disclose source policy in GPL v2.0 License, you should include a citation in your paper.
 
As the program is a part of JetController's system, please consider citing JetController for disclosing source if suitable.

## Citation for Other Purpose (Patent / Self-media / Youtuber / Business Usage)

Be aware that you should follow the GPL v2.0 License, so you have to disclose source.

Please cite the repository to disclose source.

Also, it is welcome to cite and introduce our acadamic papers, JetController, if possible.

# How to use this

As soon as you execute the program, the program will automatically find your Steam location.

## Game Mod
To detect the game events, the program creates a game mod, including **show events** and **cheats on**.

You can customize the game mod like this:

```
HalfLifeAlyx_Autoexec HLA_Autoexec =
    new HalfLifeAlyx_Autoexec().
    UnlimitedMagazineInBag(true).
    GiveAllUnlockedWeapons(true);
```

Be aware that you should setup the object before **restarting** Half-Life: Alyx.

To restart Half-Life: Alyx, just pass the mod you selected.

You are allowed to pass a null pointer.

```
HLA_Manager.RestartHalfLifeAlyx(HLA_Autoexec);
```

The program will kill running Half-Life: Alyx and start a new instance.

## In-game Commands

Some mods are only available when your game is loaded, such as God Mode and teleporting to other maps.

You can use the code to achieve that.

```
GameMonitor
    .SetBuddha(true) //God Mode
    .Map("a1_intro_world");	//Switch Maps
```

## Event Detectors

Please modify these function for actuating your haptic devices.

```
	static void ShootHapticFeedback(NowWeaponType WeaponType);
	static void SwitchWeaponHapticFeedback(NowWeaponType WeaponType);
	static void InsertBulletHapticFeedback(NowWeaponType WeaponType);
	static void GloveStartGrab(bool RightHand);
	static void GloveStopGrab(bool RightHand);
	static void StartTeleportation();
	static void EnergygunEjectClip();
	static void EnergygunChamberedRound();
	static void Shotgun_Open();
	static void Shotgun_Close();
	static void Shotgun_Chambered();
```

# Links to JetController

CHI'21 Full Paper
https://doi.org/10.1145/3411764.3445549

CHI'21 Interactivity (Does not utilize the program) 
https://doi.org/10.1145/3411763.3451542

SIGGRAPH'21 Demo
(Not Published Yet)

Repository of JetController
https://github.com/ntu-hci-lab/JetController

Website of JetController
https://jetcontroller.org
