import React from 'react';
import { useNavigate } from "react-router-dom";
import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'
import './BusPreduzeca.css'

function BusPreduzeca(){

    const {data:preduzeca, loading, error}=Api("BusPreduzece/VratiBusPreduzeca");

    let navigate = useNavigate();

    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(preduzeca);

    return (
        <div className='busPreduzecaContainer'>
             <div className='top'>Pogledajte listu svih preduzeća</div>
            <div className='dugmeContainer'>
        {preduzeca.map(p=>
            {
                return(
                   
                                 
                    <button className='dugme' onClick={() => navigate(`/busPreduzeca/${p.id}`)}>{p.naziv}</button>
                   
                )
            })} 
        </div>

      </div>
    )
}

export default BusPreduzeca