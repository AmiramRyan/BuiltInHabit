import React, { useEffect } from 'react';
import HabitList from './components/HabitList';
import HabitForm from './components/HabitForm';

function App() {
  useEffect(() => {
    console.log('App is running');
  }, []);

  return (
    <div className="App">
      <h1>Habit Tracker</h1>
      <HabitList />
      <HabitForm />
    </div>
  );
}

export default App;
