import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Home from './pages/HomeStrana/Home.js';
import NavBar from './components/NavBar';
import BusPreduzeca from './pages/BusPreduzeca/BusPreduzeca.js';
import BusPreduzeceDetaljno from './pages/BusPreduzeca/BusPreduzeceDetaljno';
import Voznje from './pages/Voznje/Voznje.js';
import VoznjaDetaljniPrikaz from './pages/Voznje/VoznjaDetaljniPrikaz';
import ProveriRezervaciju from './pages/Rezervacija/ProveriRezervaciju';
import KreirajPutnika from './pages/Voznje/KreirajPutnika';
import Usluge from './pages/Voznje/Usluge.js'


function App() {
  return (
    <Router>
      <NavBar/>
      <Routes>
        <Route exact path="/" element={<Home/>}/>
        <Route path="/busPreduzeca" element={<BusPreduzeca/>}/>
        <Route exact path = "/busPreduzeca/:id" element = {<BusPreduzeceDetaljno/>} />
        <Route path="/voznje" element={<Voznje/>}/>
        <Route exact path = "/voznje/:id" element = {<VoznjaDetaljniPrikaz/>} />
        <Route exact path = "/KreirajPutnika/:id" element = {<KreirajPutnika/>}/>
        <Route path="/proveriRezervaciju" element={<ProveriRezervaciju/>}/>
        <Route path="usluge/:idRez" element={<Usluge/>}/>



      </Routes>
    </Router>
  );
}

export default App;
