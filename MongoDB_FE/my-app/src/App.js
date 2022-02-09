import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Home from './components/Home.js';
import NavBar from './components/NavBar';
import BusPreduzeca from './components/BusPreduzeca';
import Voznje from './components/Voznje';
import ProveriRezervaciju from './components/ProveriRezervaciju';


function App() {
  return (
    <Router>
      <NavBar/>
      <Routes>
        <Route exact path="/" element={<Home/>}/>
        <Route path="/busPreduzeca" element={<BusPreduzeca/>}/>
        <Route path="/voznje" element={<Voznje/>}/>
        <Route path="/proveriRezervaciju" element={<ProveriRezervaciju/>}/>


      </Routes>
    </Router>
  );
}

export default App;
