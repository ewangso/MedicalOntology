<?xml version="1.0" encoding="utf-8" ?>
<configuration>
</configuration>
/**
 * ChangeVisibility.cs
 * 
 * 
 * The ChangeVisibility script contains all the necessary code for the Expand Children and Collapse Children buttons
 * on the Node Menu to work. When a user clicks on a node, they prompt a menu to appear. The ChangeVisibility script
 * allow users to set the visibility of a node's child/children for easier viewing. 
*/

class ChangeVisibility {
    public:

    /**
     * CollapseChildren script makes the children of a node look invisible. 
    */
    public void collapseChildren();

    /**
     * ShowChildren script makes the children of a node become visible if they are invisible. 
    */
    public void showChildren();

    /**
     * Receives a @param node as a paramter in string form and @returns a List of all its descendant nodes.
     * 
    */
    public List<string> GetDescendants(string node, List<string> res);

};