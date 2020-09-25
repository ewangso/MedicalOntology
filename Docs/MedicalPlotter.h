<?xml version="1.0" encoding="utf-8" ?>
<configuration>
</configuration>
/**
 * The script MedicalPlotter.cs is used to parse through the CSV file, initialize the data into Unity Game Objects, and sorts the nodes in a 
 * neat formation. The nodes are put into a conical formation at a collapsed state, meaning the nodes appear invisible unless prompted to appear 
 * by the user. 
*/


class MedicalPlotter
{
public:
/**
 *Called before first update frame. 
 * 
 * 
 * 
*/
void Start();

/**
 * Converts string ID of a node into a GameObject
 * @param nodeName ID of a node
 * @returns GameObject object of node
*/
public GameObject findNode(string nodeName);

/**
 * Sets the position of a node's linerenderer to its children node's positions.
 * 
*/
public void DrawLines();

/**
 * Takes a list of game objects and places them in a perfect circle no matter the number of objects.
*/
IEnumerator CirculateNodes(List<GameObject> objects);

/**
 * Is run once per frame. Calls DrawLines().
*/
private void FixedUpdate();

/**
 * Creates a circle to place objects on.
 * @param center Where the center position of the circle will be.
 * @param radius The radius of the circle.
 * @param angle The angle (in radians) on the circle in which an object will be placed.
*/
Vector3 RandomCircle(Vector3 center, float radius, float ang);

/**
 * Takes a node and sets its layer number as well as its parent's layer number recursively.
 * @param leaf: a leaf node
 * @param layer: layer number associated with leaf
 * @returns void
*/
public void setLayers(GameObject leaf, int layer);




};
