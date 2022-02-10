import React from 'react'
import Api from '../../services/Api.js';
import Spinner from '../../components/Spinner.js';
import '../Voznje/Voznje.css'

function BusPreduzeceNaziv({id}) {

    const {data:busPreduzece, loading:load2, error:err2}=Api("BusPreduzece/VratiBusPreduzece/"+id);

    if(err2) throw err2;
    if(load2) return <Spinner/>

    console.log(busPreduzece);
    return (
        
            <h3 className='slova'>
                Naziv: {busPreduzece.naziv}
            </h3>
        
    )
    
}

export default BusPreduzeceNaziv