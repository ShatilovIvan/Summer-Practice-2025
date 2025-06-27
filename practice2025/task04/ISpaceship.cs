namespace task04;

public interface ISpaceship
{
    string MoveForward();      
    string Rotate(int angle);  
    string Fire();           
    int Speed { get; }      
    int FirePower { get; }   
}