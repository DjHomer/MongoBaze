import React, {useState} from 'react'
import {useParams} from "react-router-dom";
import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'


function Usluge(){
    const {idRez}=useParams();

    const {data:usluge, loading, error}=Api("Usluga/VratiUsluge");
    //const {data:rezervacije, loading2, error2}=Api("Rezervacija/VratiRezervacije");
    const [showSpinner,setShowSpinner]=useState(false);
    const [dodateUsluge,setDodateUsluge]=useState([]);

    if(error) throw error;
    if(loading) return <Spinner/>

    //if(error2) throw error2;
    //if(loading2) return <Spinner/>

    console.log(usluge)
    //console.log(rezervacije)

    return(
        <div className={"formCreate"} class="float-container" style={{textAlign:"center"}}>
            {usluge.map(el=>{
                return (<div className={"formCreate"} class="float-container" style={{textAlign:"center"}}>
                    <div class="float-child" style={{width:"10%"}}>
                    <input type="checkbox" onChange={(event)=>{
                            if(event.currentTarget.checked){
                                dodateUsluge.push(el.id);
                            }
                            else{
                                let index=dodateUsluge.indexOf(el.id);
                                if(index!=-1){
                                    dodateUsluge.splice(index,1);
                                }
                            }
                            console.log(dodateUsluge);
                    }}/>
                    </div>
                    <div class="float-child" style={{width:"30%"}}>
                    <p style={{color:"#3399FF"}}>Naziv: {el.naziv}</p><br/>
                    </div>
                    <div class="float-child" style={{width:"30%"}}>
                    <p style={{color:"#3399FF"}}>Cena: {el.cena}</p><br/>
                    </div>
                    <div class="float-child" style={{width:"30%"}}>
                    <img src={"data:image/jpg;base64,"+el.slikaBytesBase} style={{"width":"100px","height":"100px"}}/>
                    </div>
                </div>);
            })}
            <div class="float-child" style={{width:"100%"}}>
            <button class="btn btn-primary" type="button" onClick={()=>DodajUslugeRezervaciji()}>Potvrdi</button> </div>
        </div>

    );

    function DodajUslugeRezervaciji(){
        setShowSpinner(true);
        fetch("https://localhost:44399/Rezervacija/DodajUsluge/"+idRez,{
            method:"PUT",
            headers:{"Content-Type":"application/json"},
            body: JSON.stringify(dodateUsluge)
        }).then(p=>{
            if(p.ok){
                //window.location.replace("../rezervacija/"+idRez);
                console.log("Uspesno dodato!")
            }
        }).catch(exc=>{
            console.log(exc);
        })
        setShowSpinner(false);

    }

}

export default Usluge