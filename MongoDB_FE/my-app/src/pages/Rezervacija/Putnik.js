import React from 'react'
import '../Rezervacija/ProveriRezervaciju.css';
import { Button } from '../../components/Button'
import  {useState} from 'react';

function Putnik() {
    const[putnikJmbg, setPutnikJmbg]=useState("");
    const [ime,setPutnikIme]=useState("");
    const [prezime,setPutnikPrezime]=useState("");
    const [email,setPutnikEmail]=useState("");
    return (
        
        <div className="rezervacijaContainer">
            <h1 className="Naslov"> Detaljne informacije o korisniku rezervacije</h1>
            <div className="formContainer">
                <label className="labelica">Radi dodatne autentifikacije, unesite vaš Jmbg:</label>
                <input className="inputCode" type="text" onChange={(event)=>setPutnikJmbg(event.currentTarget.value)} />
            </div>
            <div className="buttonRez">
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--medium'
                onClick= {()=>Proveri()}
                
                >
                Prikaži
                </Button>
                

            </div>
             <div className="elements">
                <h3 className="Naslovic">
                    Ime: {ime}
                </h3>
                <h3 className="Naslovic">
                    Prezime: {prezime}
                </h3>
                <h3 className="Naslovic">
                    Email: {email}
                </h3>
             </div>
            

        </div>
           
            
        
    );
    function Proveri(){
        fetch("https://localhost:44335/Putnik/VratiPutnikaJmbg/"+putnikJmbg).then(put=>{
            if(put.ok){
                put.json().then(dataa=>{
                   setPutnikIme(dataa.ime);
                   setPutnikPrezime(dataa.prezime);
                   setPutnikEmail(dataa.email);
                   
                    
                   
                })
                

            }else{
                
                alert("Nisu pronadjeni podaci!");
               
            }
        });
    }    
}

export default Putnik