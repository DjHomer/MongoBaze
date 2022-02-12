import React from 'react'
import {useParams} from "react-router-dom";
import Spinner from '../../components/Spinner.js';
import Api from '../../services/Api.js';
import '../Voznje/Voznje.css'
import BusPreduzeceNaziv from './BusPreduzeceNaziv.js';
import { Link, NavLink } from "react-router-dom";
import moment from 'moment';

function VoznjaDetaljniPrikaz() {

    const {id}=useParams();

    const {data:v, loading:load1, error:err1}=Api("Voznja/VratiVoznju/"+id);
    const{data:putnici, loading:load2, error:err2} = Api("Rezervacija/VratiRezervacije")

    if(err1) throw err1;
    if(load1) return <Spinner/>;

    if(err2) throw err2;
    if(load2) return <Spinner/>;

    console.log(v);
    console.log(putnici);
  
    console.log(moment().isBefore(v.datumVoznje));
    return (
        <div className={"form"}  style={{textAlign:"left"}}>
            <div className={"naslov"}  style={{width:"100%"}}>
            <h2>Ovde možete videti detaljne informacije o vožnji:</h2> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Polazni grad: {v.polazniGrad}</h3> </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Dolazni grad: {v.dolazniGrad}</h3> </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Datum: {v.datumVoznje}</h3> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Tip vožnje: {v.tipVoznje}</h3> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Cena vožnje: {v.cenaVoznje} din.</h3> 
            </div>
            <div>

            </div>
            
            <div className='dugmici' style={{width:"100%"}}>
                {v.busPreduzece!=="" && <BusPreduzeceNaziv id={v.busPreduzece}/>}
                
                <div className="float-child" style={{width:"250%"}}>
                {v.busPreduzece!=="" && <Link to={`/busPreduzeca/${v.busPreduzece}`} className="button1">Više o preduzeću</Link>} 
                </div>
                <div className="float-child" style={{width:"25%"}}>
                {moment().isBefore(v.datumVoznje) && <Link to={`/KreirajPutnika/${v.id}`} className="button1">Rezerviši</Link>} </div>    
            </div>
        </div>
    )
}

export default VoznjaDetaljniPrikaz
