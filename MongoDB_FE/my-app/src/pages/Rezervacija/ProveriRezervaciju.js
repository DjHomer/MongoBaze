import React from 'react';
import Spinner from '../../components/Spinner.js'
import  {useState} from 'react';
import { useNavigate } from "react-router-dom";
import Api from '../../services/Api.js'
import { Button } from '../../components/Button'
import  './ProveriRezervaciju.css'
import moment from 'moment';
import {useParams} from "react-router-dom";



function ProveriRezervaciju(){
    const[kodRez, setKodRez]=useState();
    const [showSpinner,setShowSpinner]=useState(false);
    const [brSedista,setStatusSedista]=useState("");
    const [status,setStatus]=useState("");
    const [cena,setCena]=useState("");
    
    
    return (
        <div className="rezervacijaContainer">
            <h1 className="Naslov">Na ovoj stranici možete proveriti osnovne informacije o vašoj rezervaciji!  </h1>
            {showSpinner && <Spinner />}
            <div className="formContainer">
                <label className="labelica">Unesite kod rezervacije:</label>
                <input className="inputCode" type="text" onChange={(event)=>setKodRez(event.currentTarget.value)} />
            </div>
            <div className="buttonRez">
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick= {()=>ProveriRezervacijuu()}
                
                >
                Proverite rezervaciju
                </Button>
                

            </div>
            <div className="elements">
                    <h3 className="Naslovic">Status rezervacije:{status} </h3>
                    <h3 className="Naslovic" >Broj sedista: {brSedista} </h3> 
                    <h3 className="Naslovic" >Cena reazervacije: {cena} </h3>
                
             
             </div>
             
            
               

            

                       
        </div>
        
         
    );
    function ProveriRezervacijuu(){
        setShowSpinner(true);
        fetch("https://localhost:44335/Rezervacija/VratiRezervaciju/"+kodRez).then(p=>{
            if(p.ok){
                p.json().then(data=>{
                    setStatus(data.status);
                    setStatusSedista(data.brSedista);
                    setCena(data.cena);
                    setShowSpinner(false);
                    
                    
                   
                })
                

            }else{
                setStatus("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setStatusSedista("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setCena("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setShowSpinner(false);
            }
        });

    }
    

   


}

export default ProveriRezervaciju