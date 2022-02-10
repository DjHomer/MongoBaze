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

    const {data:l, loading:load1, error:err1}=Api("Voznja/VratiVoznju/"+id);
    
    let pom="000000000000000000000000";
    if(l!==null)
        pom=l.busPreduzece;

    if(err1) throw err1;
    if(load1) return <Spinner/>;

    console.log(l);
   // console.log(busPreduzece);
   //let navigate = useNavigate(); <h5>Naziv:{busPreduzece.Naziv}</h5> 
   

    console.log(moment().isBefore(l.datumVoznje));
    return (
        <div className={"form"}  style={{textAlign:"left"}}>
            <div className={"naslov"}  style={{width:"100%"}}>
            <h2>Pogledajte detaljne informacije o vožnji</h2> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Polazni grad: {l.polazniGrad}</h3> </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Dolazni grad: {l.dolazniGrad}</h3> </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Datum: {l.datumVoznje}</h3> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Tip vožnje: {l.tipVoznje}</h3> 
            </div>
            <div className={"slova"} style={{width:"100%"}}>
            <h3>Cena vožnje: {l.cenaVoznje}</h3> 
            </div>
            <div>

            </div>
            
            <div className='dugmici' style={{width:"100%"}}>
                {l.busPreduzece!=="" && <BusPreduzeceNaziv id={l.busPreduzece}/>}
                
                <div class="float-child" style={{width:"250%"}}>
                {l.busPreduzece!=="" && <Link to={`/busPreduzeca/${l.busPreduzece}`} className="button1">Više o preduzeću</Link>} 
                </div>
                <div class="float-child" style={{width:"25%"}}>
                {moment().isBefore(l.datumVoznje) && <Link to={`/KreirajPutnika/${l.id}`} className="button1">Rezerviši</Link>} </div>    
            </div>
        </div>
    )
}

export default VoznjaDetaljniPrikaz
