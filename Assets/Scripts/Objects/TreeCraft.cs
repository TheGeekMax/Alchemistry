using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCraft{
    //array list of all the branches
    public List<TreeCraft> branches;
    public string name;

    public TreeCraft(string name){
        this.name = name;

        //on init les branches
        branches = new List<TreeCraft>();
    }

    private bool IsBranch(string name){
        foreach(TreeCraft branch in branches){
            if(branch.name == name){
                return true;
            }
        }
        return false;
    }

    private TreeCraft GetBranch(string name){
        foreach(TreeCraft branch in branches){
            if(branch.name == name){
                return branch;
            }
        }
        return null;
    }

    private TreeCraft GetFirstBranch(){
        if(branches.Count > 0){
            return branches[0];
        }
        return null;
    }

    public void Add(string name){
        if(!IsBranch(name)){
            branches.Add(new TreeCraft(name));
        }
    }

    public void AddCraft(string item1,string item2,string result){
        if(!IsBranch(item1)){
            Add(item1);
        }
        if(!IsBranch(item2)){
            Add(item2);
        }
        GetBranch(item1).Add(item2);
        GetBranch(item2).Add(item1);
        GetBranch(item1).GetBranch(item2).Add(result);
        GetBranch(item2).GetBranch(item1).Add(result);
    }

    public string GetResult(string item1,string item2){
        if(IsBranch(item1)){
            if(GetBranch(item1).IsBranch(item2)){
                return GetBranch(item1).GetBranch(item2).GetFirstBranch().name;
            }
        }
        return null;
    }
}
