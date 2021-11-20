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
@inproceedings{10.1145/3411764.3445549,
	author = {Wang, Yu-Wei and Lin, Yu-Hsin and Ku, Pin-Sung and Miyatake, Y\={o}ko and Mao, Yi-Hsuan and Chen, Po Yu and Tseng, Chun-Miao and Chen, Mike Y.},
	title = {JetController: High-Speed Ungrounded 3-DoF Force Feedback Controllers Using Air Propulsion Jets},
	year = {2021},
	isbn = {9781450380966},
	publisher = {Association for Computing Machinery},
	address = {New York, NY, USA},
	url = {https://doi.org/10.1145/3411764.3445549},
	doi = {10.1145/3411764.3445549},
	abstract = {JetController is a novel haptic technology capable of supporting high-speed and persistent 3-DoF ungrounded force feedback. It uses high-speed pneumatic solenoid valves to modulate compressed air to achieve 20-50Hz of full impulses at 4.0-1.0N, and combines multiple air propulsion jets to generate 3-DoF force feedback. Compared to propeller-based approaches, JetController supports 10-30 times faster impulse frequency, and its handheld device is significantly lighter and more compact. JetController supports a wide range of haptic events in games and VR experiences, from firing automatic weapons in games like Halo (15Hz) to slicing fruits in Fruit Ninja (up to 45Hz). To evaluate JetController, we integrated our prototype with two popular VR games, Half-life: Alyx and Beat Saber, to support a variety of 3D interactions. Study results showed that JetController significantly improved realism, enjoyment, and overall experience compared to commercial vibrating controllers, and was preferred by most participants. },
	booktitle = {Proceedings of the 2021 CHI Conference on Human Factors in Computing Systems},
	articleno = {124},
	numpages = {12},
	keywords = {ungrounded force feedback., air propulsion, High-speed haptic feedback},
	location = {Yokohama, Japan},
	series = {CHI '21}
}
```

### BibTeX for JetController SIGGRAPH'21 Demo
```
@INBOOK {10.1145/3450616.3464520,
    author    = "Wang, Yu-Wei and Lin, Yu-Hsin and Miyatake, Yoko and Tsai, Ching-Yi and Ku, Pin-Sung and Chen, Mike Y.",
    title     = "JetController: High-Speed Ungrounded 3-DoF Force Feedback Controllers Using Air Propulsion Jets",
    publisher = "Association for Computing Machinery",
    year      = "2021",
    address   = "New York, NY, USA",
    isbn      = "9781450383691",
    url       = "https://doi.org/10.1145/3450616.3464520",
    abstract  = "JetController is a novel high-speed, persistent 3-DoF ungrounded force feedback controller.It uses high-speed pneumatic solenoid valves to modulate compressed air to achievefull impulses of 50Hz at 1.0N (20Hz at 4.0N), and combines multiple air propulsionjets to generate 3-DoF ungrounded force feedback. Compared to propeller-based approaches,JetController supports 10-30 times faster impulse frequency, and its handheld deviceis significantly lighter and more compact. JetController enables a wide range of hapticevents in games and VR experiences, from firing automatic weapons in games like Halo(15Hz) to slicing fruits in Fruit Ninja in 3-DoF (up to 45Hz). To demonstrate JetController,we integrated our prototype with two best-selling VR games, Half-life: Alyx and FruitNinja VR, to highlight a variety of 3-DoF interactions that were not possible before.",
    booktitle = "ACM SIGGRAPH 2021 Labs",
    articleno = "5",
    numpages  = "2"
}
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
You then can execute Half-Life: Alyx by calling *HLA_Manager.RestartHalfLifeAlyx(HalfLifeAlyx_Autoexec)*.

## Game Mod
To detect the game events, the program have to create a game mod, including **show events** and **cheats on**, for Half-Life: Alyx.

If you want to add some cheats, you can customize the game mod like this:

```
HalfLifeAlyx_Autoexec HLA_Autoexec =
    new HalfLifeAlyx_Autoexec().
    UnlimitedMagazineInBag(true).
    GiveAllUnlockedWeapons(true);
```

Be aware that you should setup the object before **executing/restarting** Half-Life: Alyx.

To programmatically execute/restart Half-Life: Alyx, just pass the mod you selected.

If you don't wish to use other game mod, you are allowed to pass a null pointer to the function *HLA_Manager.RestartHalfLifeAlyx(NULL)*, and the program will only create minimum required game mod for game event detect purpose.

```
HLA_Manager.RestartHalfLifeAlyx(HLA_Autoexec);
```

The program will kill running Half-Life: Alyx and start a new instance.

## In-game Commands

Some mods are only available when your game is loaded, such as God Mode and teleporting to other maps.

You can use the code to achieve that.

A function *GameMonitor.WaitPlayerReloaded()* could help you detecting the game loaded event.

An example code is given to show how to teleport to a specify map after the game is fully loaded.

```
GameMonitor
    .SetBuddha(true) //God Mode
    .WaitPlayerReloaded() //Wait for game loading finished
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

## CHI'21 Full Paper

[Author-Izer Download Link](https://dl.acm.org/doi/10.1145/3411764.3445549?cid=99659702103)

(You can obtain the camera-ready version from the ACM Digital Library at no charge via Author-Izer Service Link)

## CHI'21 Interactivity (We didn't use Half-Life: Alyx in this paper) 

[Author-Izer Download Link](https://dl.acm.org/doi/10.1145/3411763.3451542?cid=99659702103)

(You can obtain the camera-ready version from the ACM Digital Library at no charge via Author-Izer Service Link)

## SIGGRAPH'21 Labs

[Download Link](https://dl.acm.org/doi/10.1145/3450616.3464520)

## Repository of JetController
https://github.com/ntu-hci-lab/JetController

## Website of JetController
https://jetcontroller.org
