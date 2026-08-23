namespace WorldLoopEdge.Content.Subworlds;

public interface ISubworldPost
{
    void BasePostEnter();
    void BasePostExit();
    void BasePreUpdate();
    void BasePostUpdate();
    void BasePlayerDeath();
}