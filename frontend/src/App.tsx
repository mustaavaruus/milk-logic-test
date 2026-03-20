import React from 'react';
import logo from './logo.svg';
import './App.css';
import MainContainer from './components/main-container/MainContainer';
import { ToastContainer } from 'react-toastify';

function App() {
  return (
    <div className="App">
      <div>
        <ToastContainer />
        <MainContainer  />
      </div>
    </div>
  );
}

export default App;
