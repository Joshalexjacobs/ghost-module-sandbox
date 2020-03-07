
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/logo.png)

> A Ghost Module sandbox environment for testing and exporting bullet patterns

**Built with [Unity 2019.3.2f1](https://unity3d.com/unity/whats-new/2019.3.2)**

## Getting Started Guide


### Creating a New Scene

**(Skip this step if you don't plan on using GIT to back up your changes!)**

1. Open the Scenes folder and click on the existing Sandbox scene.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01%20Creating%20a%20New%20Scene/step%201.png)

2. Duplicate the scene by pressing CTRL + D with it selected OR click Edit > Duplicate from the menu.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01%20Creating%20a%20New%20Scene/step%202.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01%20Creating%20a%20New%20Scene/step%203.png)

3. Rename your scene and double click it to open it. The new scene name should be visible at the top of the Hierarchy panel.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01%20Creating%20a%20New%20Scene/step%204.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01%20Creating%20a%20New%20Scene/step%205.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01%20Creating%20a%20New%20Scene/step%206.png)


### Modifying a Bullet Pattern

1. There should already be a template shot controller we can test with inside the scene
(if not, you can create a copy by going to Assets > Templates > UbhShotCtrlTemplate. Make a duplicate by selecting it and pressing CTRL + D, then drag it into the scene's hierarchy).

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02%20Modifying%20a%20Bullet%20Pattern/step%201.png)

If we play the scene as it is, we can see the current bullet pattern.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02%20Modifying%20a%20Bullet%20Pattern/step%202.png)

2. To modify the existing pattern attached to the shot controller, click the arrow next to it in the hierarchy view and select the 5WayShot.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02%20Modifying%20a%20Bullet%20Pattern/step%203.png)

3. On the right side of the editor, the Inspector panel should display the nWay Shot script attached to this object. 

Modifying any of the following values should change up the pattern:
- Bullet Num
- Bullet Speed
- Accelaration Speed
- Way Num
- Center Angle
- Between Angle
- Next Line Delay (in seconds)

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02%20Modifying%20a%20Bullet%20Pattern/step%204.png)


### Adding More Patterns

1. Copy the existing pattern by selecting it and hitting CTRL + D.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%201.png)

This should create 5WayShot (1)

2. Click the UbhShotCtrlTemplate object in the hierarchy.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%202.png)

Update the Shot List size element to be 2 instead of 1.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%203.png)

Drag the newly duplicated shot (5WayShot (1)) into the new element 1 slot.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%204.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%205.png)

3. Click 5WayShot (1) and edit any values in the inspector.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%206.png)

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03%20Adding%20More%20Patterns/step%207.png)

Hit Play to test out the new shot combination.


### Ubh Example Patterns

More patterns can be found under [Assets > Templates > Library Examples](https://github.com/Joshalexjacobs/ghost-module-sandbox/tree/master/Assets/Templates/Library%20Examples).

Many of these patterns include a variety of different shots that use scripts outside of nWayShot, but require the exact same set up in order to get working.


### Exporting and Sharing Patterns

1. First we want to create an original prefab out of this, so click and drag the entire UbhShotCtrlTemplate into the Assets folder below.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/04%20Exporting%20and%20Sharing%20Patterns/step%201.gif)

When the prompt appears, click Original Prefab.

Once that's done, rename the prefab and then right click on it and select Export Package...

(Make sure you deselect **Include dependencies** during this step or your package will be much bigger than it needs to be!)

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/04%20Exporting%20and%20Sharing%20Patterns/step%202.gif)

Save the unity package and then you're done!
