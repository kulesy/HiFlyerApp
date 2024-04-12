using HiFlyerClassLibrary.Models;

namespace HiFlyerWebApp.Stores
{
    public class ErrorListState
    {
        public List<ErrorModel> Errors { get; }
        public ErrorListState(List<ErrorModel> errors)
        {
            Errors = errors;
        }
    }
    public class ErrorListStore
    {
        private ErrorListState _state;
        public ErrorListStore()
        {
            _state = new ErrorListState(new());
        }
        public ErrorListState GetState()
        {
            return _state;
        }

        public void AddError(ErrorModel error)
        {
            var currentErrors = GetState().Errors;
            if (currentErrors == null)
                currentErrors = new List<ErrorModel>();
            currentErrors.Add(error);
            _state = new(currentErrors);
            BroadcastStateChange();
        }

        public void ClearMessage()
        {
            _state = new ErrorListState(null);
            BroadcastStateChange();
        }

        //////////////////

        private Action _listeners;
        public void AddStateChangeListeners(Action listener)
        {
            _listeners += listener;
        }
        public void RemoveStateChangeListeners(Action listener)
        {
            _listeners -= listener;
        }

        public void BroadcastStateChange()
        {
            _listeners.Invoke();
        }
    }
}
