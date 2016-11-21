/*
 * File: StateMachine.cs
 * Desc
 *  : 상태와 연관된 모든 데이터와 메소드들을 캡슐화하여 관리
 *  : Entity를 상속받는 오브젝트(Player, Enemy, Etc..)들은
 *      상태기계의 인스턴스를 소유하고, 상태기계에게 상태관리를 위임
 */
public class StateMachine<T> where T : Entity
{
    T owner_entity;
    State<T> current_state;
    State<T> previous_state;
    State<T> global_state;

    public void Init(T _owner, State<T> _state)
    {
        owner_entity = _owner;
        current_state = null;
        previous_state = null;
        global_state = null;

        ChangeState(_state);
    }
    public void Execute()
    {
        if (current_state != null)
            current_state.Execute(owner_entity);

        if (global_state != null)
            global_state.Execute(owner_entity);
    }
    public void ChangeState(State<T> _state)
    {
        if (_state == null) return;
        if (current_state != null)
        {
            current_state.Exit(owner_entity);
            previous_state = current_state;
        }
        current_state = _state;
        current_state.Enter(owner_entity);
    }
    public void RevertToPreviousState()
    {
        ChangeState(previous_state);
    }
    public State<T> GetCurrentState()
    {
        return current_state;
    }
    public void Set_GlobalState(State<T> _state)
    {
        global_state = _state;
    }
}
