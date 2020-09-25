<?xml version="1.0" encoding="utf-8" ?>
<configuration>
</configuration>
/**
 * The Collide.cs script is attached to each node and its purpose is to force nodes apart if they are within too close of a distance to each other. 
 * This prevents nodes from overlapping and having the same position.
*/

class Collide {
    public:

/**
 *Called before first update frame. 
*/
void Start();

/**
 * This is called when two nodes' colliders are touching. If the collision node is on the right side of the current node, current node will
 * move left. If the collision node is on the left side, current node will move right. 
*/
public void OnTriggerStay(Collider collision);

/**
 * Called once per frame. Sets position of each node on top of its children nodes. 
*/
void FixedUpdate();

}