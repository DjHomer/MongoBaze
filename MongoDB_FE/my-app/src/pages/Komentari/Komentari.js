import React, {useState} from 'react'
import Spinner from '../../components/Spinner.js';
import Api from '../../services/Api.js';
import KreirajKomentar from './KreirajKomentar.js';
import ReactStars from "react-rating-stars-component";

function Komentari({busPreduzece}) {

    const {data:komentari, loading:loading, error:error}=Api("Komentar/VratiKomentareZaBusPreduzece/" + busPreduzece);
    const [prikaziKreirajKomentar, setPrikaziKreirajKomentar]=useState(true);
    const [ime, setIme]=useState("");
    const [prezime, setPrezime]=useState("");
    const [tekst, setTekstKomentara]=useState("");
    const [zvezdice, setBrZvezdica]=useState(0);

    if(error) throw error;
    if(loading) return <Spinner/>;

    //let komentari=kom.filter(k=>k.)
    console.log(komentari);

    const tekstDugme=prikaziKreirajKomentar===true ? "Dodaj komentar" : "Zatvori";

    const handleStateChange=(ev)=>{
        console.log(ev.target.name);
        if(ev.target.name==="ime")
            setIme(ev.target.value);
        if(ev.target.name==="prezime")
            setPrezime(ev.target.value);
        if(ev.target.name==="tekst")
            setTekstKomentara(ev.target.value);
    }

    const promeniZvezdice=(broj)=>{
        setBrZvezdica(broj);
    }

    const brKomentara = komentari.length > 0 ? komentari.length + " komentara" 
                        : komentari.length === 1 ? komentari.length + " komentar" 
                        : "Nema komentara"
    console.log(komentari.length)

   

    return (


    <div class="komentari">
  
                <div className='header'>
                    <h3>{brKomentara}</h3>
                    <button className='dugmence' onClick={()=>setPrikaziKreirajKomentar(!prikaziKreirajKomentar)}>{tekstDugme}</button>
                    {!prikaziKreirajKomentar && <KreirajKomentar 
                                                ime={ime}
                                                prezime={prezime} 
                                                tekst={tekst} 
                                                zvezdice={zvezdice}
                                                handleStateChange={handleStateChange}
                                                promeniZvezdice={promeniZvezdice}
                                                busPreduzece={busPreduzece}
                                                />}
                </div>
                {komentari.map(k=>
                {   
                    return(
                    
                            <div className='komentar'>
                                 <h4>{k.ime} {k.prezime}</h4>
                                 <div>{k.tekst}</div>       
                                 <ReactStars
                                    count={5}
                                    value={k.zvezdice}
                                    size={24}
                                    activeColor="#ffd700"
                                />
                            </div>
                        
                )})}
            
       
    </div>

    )
}

export default Komentari
