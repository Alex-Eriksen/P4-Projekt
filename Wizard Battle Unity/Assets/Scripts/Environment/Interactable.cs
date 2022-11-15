public interface IInteractable
{
    public bool IsInteractable { get; }
    public void EnterRange();
    public void ExitRange();
    public void Interact(Entity entity);
}
