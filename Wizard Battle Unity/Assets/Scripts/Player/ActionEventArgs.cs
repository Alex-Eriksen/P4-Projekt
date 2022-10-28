using System;

public class ActionEventArgs : EventArgs
{
    public static new readonly ActionEventArgs Empty;

    public readonly ActionEventArgsFlag Flag;

    public ActionEventArgs(ActionEventArgsFlag flag = ActionEventArgsFlag.None)
    {
        Flag = flag;
    }
}