import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Home from './pages/HomeStrana/Home.js';
import NavBar from './components/NavBar';
import BusPreduzeca from './pages/BusPreduzeca';
import Voznje from './pages/Voznje';
import ProveriRezervaciju from './pages/Rezervacija/ProveriRezervaciju';


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
