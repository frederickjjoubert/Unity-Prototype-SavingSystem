public interface ISaveable
{
    string CaptureState();
    void RestoreState(string state);
}