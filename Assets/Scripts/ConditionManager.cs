using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public static ConditionManager instance;

    private Dictionary<string, Condition> mUniqueValueMap;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Multiple Condition Manager exists, only 1 should be allowed!");

        mUniqueValueMap = new Dictionary<string, Condition>();
    }

    public Condition AddCondition(Condition newCondition)
    {
        return AddConditionWithParent(newCondition, null, 0);
    }

    // call this when event with uniqueTriggerName is completed to update all statify conditions
    public void UpdateConditions(string uniqueTriggerName)
    {
        if (mUniqueValueMap.ContainsKey(uniqueTriggerName))
            mUniqueValueMap[uniqueTriggerName].UpdateConditions(uniqueTriggerName);
    }

    public Condition GetConditionWithName(string uniqueTriggerName)
    {
        return mUniqueValueMap[uniqueTriggerName];
    }

    private Condition AddConditionWithParent(Condition newCondition, Condition parentCondition, int position)
    {
        if (mUniqueValueMap.ContainsKey(newCondition.GetUnqiueValue()))
        {
            if (parentCondition == null)
                return newCondition;
            else
                parentCondition.SetSubConditionAt(mUniqueValueMap[newCondition.GetUnqiueValue()], position);
        }
        else
        {
            if (newCondition.IsLeafNode())
            {
                mUniqueValueMap[newCondition.GetUnqiueValue()] = newCondition;
            }
            else
            {
                for (int index = 0; index < newCondition.GetSubConditionCount(); index++)
                    AddConditionWithParent(newCondition.GetSubConditionAt(index), newCondition, index);
            }

            return newCondition;
        }

        return null;
    }
}

public class Condition
{
    private bool mIsLeafNode;
    private bool mIsCompleted;

    // unique condition name that identify the event that completes the condition if it is a leaf node
    private string mUniqueTriggerName;

    private Dictionary<string, Condition> mParentConditions;
    private List<Condition> mSubConditions;
    private int mProgressIndex;

    // default constructor creates a leaf node with empty
    public Condition(string uniqueTriggerName) : this(uniqueTriggerName, null) { }


    public Condition(string uniqueTriggerName, List<Condition> subConditions) {
        mSubConditions = subConditions;
        mUniqueTriggerName = uniqueTriggerName;

        if (subConditions == null || subConditions.Count == 0)
        {
            mIsLeafNode = true;
        }
        else
        {
            mIsLeafNode = false;
            foreach (Condition subCondition in subConditions)
                subCondition.AddParentCondition(this);
        }

        mParentConditions = new Dictionary<string, Condition>();
        mIsCompleted = false;
        mProgressIndex = 0;
    }

    public void AddParentCondition(Condition parentCondition) {
        if (mParentConditions == null)
            mParentConditions = new Dictionary<string, Condition>();
        if (mParentConditions.ContainsKey(parentCondition.GetUnqiueValue()))
            return;

        mParentConditions[parentCondition.GetUnqiueValue()] = parentCondition;
    }


    public void AddSubCondition(Condition condition) {
        if (mSubConditions == null)
            mSubConditions = new List<Condition>();

        if (mSubConditions.Count == 0)
            mIsLeafNode = false;

        mSubConditions.Add(condition);
    }

    public void AddSubCondition(string uniqueTriggerName) {
        if (mSubConditions == null)
            mSubConditions = new List<Condition>();

        if (mSubConditions.Count == 0)
            mIsLeafNode = false;

        mSubConditions.Add(new Condition(uniqueTriggerName));
    }

    public void UpdateConditions(string uniqueTriggerName) {
        if (mIsCompleted) return;
        if (!mIsLeafNode && !mSubConditions[mProgressIndex].GetUnqiueValue().Equals(uniqueTriggerName)) return;

        if (mIsLeafNode)
            mIsCompleted = true;
        else
        {
            mProgressIndex++;
            if (mProgressIndex >= mSubConditions.Count)
                mIsCompleted = true;
        }

        if (mIsCompleted) {
            // loop through parent conditions
            if (mParentConditions == null) return;
            foreach (var parentCondition in mParentConditions)
                mParentConditions[parentCondition.Key].UpdateConditions(uniqueTriggerName);
        }
    }

    public string GetUnqiueValue() {
        return mUniqueTriggerName;
    }

    public bool IsCompleted() {
        return mIsCompleted || (!mIsLeafNode && mProgressIndex > mSubConditions.Count);
    }

    public bool IsLeafNode() {
        return mIsLeafNode;
    }

    public int GetSubConditionCount() {
        if (mSubConditions == null)
            return 0;
        else
            return mSubConditions.Count;
    }

    public Condition GetSubConditionAt(int position) {
        if (mSubConditions == null || position < 0 || position >= mSubConditions.Count)
            return null;

        return mSubConditions[position];
    }

    public void SetSubConditionAt(Condition newSubCondition, int position)
    {
        if (mSubConditions == null || position < 0 || position >= mSubConditions.Count)
            return;

        mSubConditions[position] = newSubCondition;
    }

    public int GetCurrentProgressIndex() {
        return mProgressIndex;
    }
}
