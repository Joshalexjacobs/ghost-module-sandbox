
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/logo.png)

> A Ghost Module sandbox environment for testing and exporting bullet patterns

## Getting Started Guide


### Creating a New Scene

**(Skip this step if you don't plan on using GIT to back up your changes!)**

1. Open the Scenes folder and click on the existing Sandbox scene.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01 Creating a New Scene/step 1.png)

2. Duplicate the scene by pressing CTRL + D with it selected OR click Edit > Duplicate from the menu.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01 Creating a New Scene/step 2.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01 Creating a New Scene/step 3.png)

3. Rename your scene and double click it to open it. The new scene name should be visible at the top of the Hierarchy panel.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01 Creating a New Scene/step 4.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01 Creating a New Scene/step 5.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/01 Creating a New Scene/step 6.png)


### Modifying a Bullet Pattern

1. There should already be a template shot controller we can test with inside the scene
(if not, you can create a copy by going to Assets > Templates > UbhShotCtrlTemplate. Make a duplicate by selecting it and pressing CTRL + D, then drag it into the scene's hierarchy).

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02 Modifying a Bullet Pattern/step 1.png)

If we play the scene as it is, we can see the current bullet pattern.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02 Modifying a Bullet Pattern/step 2.png)

2. To modify the existing pattern attached to the shot controller, click the arrow next to it in the hierarchy view and select the 5WayShot.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02 Modifying a Bullet Pattern/step 3.png)

3. On the right side of the editor, the Inspector panel should display the nWay Shot script attached to this object. 

Modifying any of the following values should change up the pattern:
- Bullet Num
- Bullet Speed
- Accelaration Speed
- Way Num
- Center Angle
- Between Angle
- Next Line Delay (in seconds)

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/02 Modifying a Bullet Pattern/step 4.png)


### Adding More Patterns

1. Copy the existing pattern by selecting it and hitting CTRL + D.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 1.png)

This should create 5WayShot (1)

2. Click the UbhShotCtrlTemplate object in the hierarchy.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 2.png)

Update the Shot List size element to be 2 instead of 1.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 3.png)

Drag the newly duplicated shot (5WayShot (1)) into the new element 1 slot.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 4.png)
# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 5.png)

3. Click 5WayShot (1) and edit any values in the inspector.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 6.png)

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/03 Adding More Patterns/step 7.png)

Hit Play to test out the new shot combination.


### Exporting and Sharing Patterns

1. First we want to create an original prefab out of this, so click and drag the entire UbhShotCtrlTemplate into the Assets folder below.

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/04 Exporting and Sharing Patterns/step 1.gif)

When the prompt appears, click Original Prefab.

Once that's done, rename the prefab and then right click on it and select Export Package...

# ![GhostModuleSandbox](https://github.com/Joshalexjacobs/ghost-module-sandbox/blob/master/Assets/Other/04 Exporting and Sharing Patterns/step 2.gif)

Save the unity package and then you're done!