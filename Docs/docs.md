# Medical Ontology Documentation

## Overview
In medical research, an ontology is defined as the structural representation of medical terminologies and the relationships between them. This project is a 3-D representation of an actual Medical Ontology. The current ontology is a representation of a small testing database, but it is projected to be used for a much larger one. For this particular visualization style, the terminologies of the ontology are represented by nodes placed in a tree-like structure. Each node represents a medical terminology, and the edges represent the relationship between them. Each node contains it's own specialized infromation which is represented with a UI. This project is meant to be used in a Virtual Reality environment. Most of the scripts are written in C# and the full project is edited in Unity. 

## Getting Started
**Follow these steps to get Unity set up and understand the project better.**
Before continuing, make sure you have basic knowledge of Unity

1. Make sure you have Unity installed (Click [here to download] (https://unity3d.com/get-unity/download)). The latest version should be fine. If the project was last worked on in an older version, Unity may change some files. Because of this, you may get errors. 
2. Once you have Unity installed, download a copy of the MedicalOntologyProject from [here] (https://git.njit.edu/ew84/MedicalOntologyProject). It is recommended that you clone the repository instead of just downloading the files. This way, you have a local repository for version control . For more on git, read [here] (https://git-scm.com/).
3. Go to Unity Hub and click 'Add'. Then, select the folder that contains the Medical Ontology project. 
4. Select the latest Unity version, and click on the project name to open Unity editor.
5. Under the 'Project' tab, there should be several folders. Open the folder named 'Scenes' under 'Assets' and click on MainScene. This should load up all the necessary components and objects for the game to work. 

### Quick Guide of Different Unity Tabs
**Scene** is where the setup of the game is viewed. You can choose a particular gameobject to focus on and view it in 3D or 2D. <br>
**Console** is where any of your scripting errors can be view. Note: if any of your script have an error, the project will not run by pressing the play button. You'll have to fix every error before you can run it again to see the game working. <br>
**Game** is what you actually use to see how the project looks after pressing the play button on your project. This tab display the final result of all your scripts and unity configuration. <br>
**Hierarchy** is where you can create different types of game objects (ex: Shapes, UI, Text) and put them in an order you'd like. Here, the gameobjects can be nested within one another, which helps organize more complex object/UI. <br>
**Project** is where you can see all the files you have for the project. The most important folders are "Resource" (contains files that have the  database in a CSV file), "Prefabs" (contains "blueprint" of gameobjects that can used to make clones of them several time), and "Scripts" (contains all C# scripts to add more functionality to your game). <br>
**Inspector** is where you can add componaets to a particular gameobject. <br>

### Project Components
**OVRPlayerController** is the object that the player/user controls. When the player moves, this is the object that moves according to the user's keyboard input. <br>
**Main Camera** is the main camera that sees what the player will see in game. It has a script that sets its position to OVRPlayerController's position once per frame. So everytime OVRPlayerController moves, the main camera follows. <br>
**Plotter** is an empty GameObject. It holds the scripts that initialize the nodes and sort them. <br>
**Menu** is the main menu that the user sees when the game starts. Additionally, if the user presses C, the menu will appear. <br>
**Menu Creator** is what hold the scipt to generate the UI menu and populate it with the information from the CDVO.csv file. <br>

### Prefabs
In Unity, a prefab is a model of a GameObject. Instead of creating a thousand different GameObjects in Unity for nodes, we used a prefab instead. If we change the style of the node prefab, then all the node objects change as well. The node objects do not appear in the hierarchy, unless the game is running. This is because the node objects are created in the MedicalPlotter script, which is attached to the Plotter object. MedicalPlotter receives data from a CSV file and creates node *objects* from the node *prefab*. All prefabs are placed inside the 'Prefabs' folder under 'Assets'.

### Testing
To try out the game within Unity Editor, click the play button on the very top. Initially, all nodes in the game are formed in a straight line. During this time, the nodes are spreading each other out to avoid cluster. After about a minute, the nodes will then form a cylindrical shape. All nodes are initially invisible or "collapsed" except nodes without parents and the root node. To make the nodes appear, click on the root or any visible node and a menu will appear. On the menu, click 'Expand Children' and that node's children will appear. 

This is how the nodes should like when they are all expanded.<br>
Game View:
![Circle](https://i.imgur.com/i8RvcWe.png)<br><br>
Scene View:
![Circle](https://i.imgur.com/RNdL2R9.png)<br>

### Node Menu
This is what a node menu looks like. When a user clicks on a node, this menu will appear. The menu allows users to view detailed information about the node, and perform other functionalities on the node. <br>
![Menu](https://imgur.com/CsCp3I7.png)<br>

### Nod Menu - View Details
When the "View Details" button is clicked the menu will show up which allows the user to view informatin like what the parent of the node is, the children, the discription of the node, the class ID, and more.

View Detials:
![Menu](https://imgur.com/ha3RMF7.png)<br>
Next Page:
![Menu](https://imgur.com/cxc2I7e.png)<br>

## Nodes
All the data for the nodes come from the CVDO.csv file containing information about a node's parent, children, description and more. Parent and children nodes are related to each other. Child nodes can be considered as subtype terminologies of a parent node. Each node also has an ID, preferred label, and definition. An ID is how each node is idenfitied. The label is how the node will be named in the Unity UI. Although a proper labelling standard has not been determined for this particular project yet, there will be one in the future. Lastly, the definition of a node is the definition of the node's medical terminology. 

## MedicalPlotter
MedicalPlotter is the main script used to initialize and sort the nodes. All data from the CSV file is parsed by this script and then placed in the Unity3D scene as GameObjects. The nodes are formed in a cylindrical shape. Nodes who have the same y-coordinate make up a layer. The layers of nodes are assigned based off of how many children and parents they have. All leaf nodes are nodes that have no children. They are assigned layer 0 and therefore, are placed at the bottom row of the structure. The root node is placed at the top layer. In order to sort the nodes correctly, all the nodes within each layer are placed adjacent to each other if they are siblings. This means that they have the same parent. Their parent node is then placed one layer above the nodes directly on top of its children. 

## Collide
Collide is a script attached to each node. It helps to make sure that nodes do not overlap and overcrowd each other. Before nodes are placed in a circle formation, the nodes are placed in a line to ensure that nodes within the circle do not overcrowd each other. The estimated amount of time it takes for all nodes to separate from each other is about 60 seconds. After 60 seconds, the nodes are then re-sorted into a cylinder shape. This is done through coroutines in Unity, which allow for functions to be called after a certain amount of time.<br />
Additionally, this script sets the position of a parent node to its middle child's position once per frame. This ensures that the parent is always in close proximity to its children. This makes it easier, since MedicalPlotter only has to sort the very bottom layer of nodes and the above layers will sort themselves.   

## ChangeVisibility
ChangeVisibility script allows the user to enable and disable nodes for easier viewing. The ChangeVisibility script powers the Expand and Collapse Children buttons on the a node's menu object. Every node has a menu object that allows the user to perform different functionalities. Collapse Children minimizes the node's children nodes, making them invisible to the user. Expand Children maximizes the node's children, making them visible to the user. The purpose of this script is to allow the user to simplify the graphical structure of the ontology to their liking. 


