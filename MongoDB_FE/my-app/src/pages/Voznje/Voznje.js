import React, {useState} from 'react';
import { useNavigate } from "react-router-dom";

import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'
import './Voznje.css'

function Voznje(){

    const [tipVoznje, setTipVoznje]=useState("sve");

    let navigate = useNavigate();
    let pathUpita="Voznja/VratiSveVoznje";
    if(tipVoznje==="gotove")
        pathUpita="Voznja/VratiSveGotoveVoznje";
    if(tipVoznje==='trenutne')
        pathUpita="Voznja/VratiSveAktivneVoznje";
    const {data:voznje, loading, error}=Api(pathUpita);

    if(voznje){
        voznje.forEach(element => {
        console.log(typeof(element.datumVoznje))
        let datum = new Date(element.datumVoznje)
              .toISOString()
              .replace(/T/, " ")
              .replace(/\..+/, "");

            
        
        
        element.datumVoznje = datum;
        
        
    });

    }   
    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(voznje);


    return (
        <div classname ="parent" style={{textAlign:"center"}}>
            <div className = "searchbar" className="float-child" style={{width:"60%"}}>
            <select style={{width:"100%", height:"30px"}} className="form-control" value={tipVoznje} onChange={(ev)=>setTipVoznje(ev.target.value)}>

                <option key={"sve"} value={"sve"}>Sve voznje</option>
                <option key={"gotove"} value={"gotove"}>Zavrsene</option>
                <option key={"trenutne"} value={"trenutne"}>Voznje na raspolaganju</option>
            </select>
            </div>
        <div classname = "tabele" className="float-child" style={{width:"100%"}}>
        {voznje.map(v=>
            {
                return(
                    <table className = "tabela" >
                        <tbody>
                            <tr>
                                <td>Polazni grad: {v.polazniGrad}</td>
                            </tr>
                            <tr>
                                <td>Dolazni grad: {v.dolazniGrad}</td>
                            </tr>
                            <tr>
                                <td>Datum: {v.datumVoznje}</td>
                            </tr>
                            <tr>
                                <td>
                                    <button class="button1" onClick={() => navigate(`/voznje/${v.id}`)}>Saznaj vise</button>
                                </td>
                            </tr>

                        </tbody>
                       

                    </table>
                    


                )
            })} 
        

      </div>
      </div>
    )
}

export default Voznje

