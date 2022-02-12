import React from 'react';
import {useParams} from "react-router-dom";
import Spinner from '../../components/Spinner.js'
import Api from '../../services/Api.js'
//import Komentari from './Komentari.js';


function BusPreduzece() {

    const {id}=useParams();

    const {data:busPreduzece, loading:loading1, error:error1}=Api("BusPreduzece/VratiBusPreduzece/"+id);


    if(error1) throw error1;
    if(loading1) return <Spinner/>;

    console.log(busPreduzece);


    return (
        <div className='detaljnoContainer'>
            <div className='infoContainer'>

                <h1 className='naziv'>{busPreduzece.naziv}</h1>

                <div className='sveInfo'>
                <div className='info'>
                    <div className='labela'>Godina osnivanja: </div>
                    <div className='data'>{busPreduzece.godinaOsnivanja}</div>                    
                </div>

                <div className='info'>
                    <div className='labela'>Grad predstavništva: </div>
                    <div className='data'>{busPreduzece.gradPredstavnistva}</div>
                    
                </div>

                <div className='info'>
                    <div className='labela'>Opis: </div>
                    <div className='data'>{busPreduzece.opis}</div>
                    
                </div>
                </div>
                

            </div>
        

            <div>
                <h2>Komentari</h2>
            </div>
        
        </div>  
        
    )
}

export default BusPreduzece
