import "./Body.css";
import Top from "./Top Section/Top";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import {useEffect, useRef, useState} from "react";
import {CityModel} from "../../../../Models/CityModel.ts";
import {Form} from "react-router-dom";

const Body = () => {
    const axiosPrivate = useAxiosPrivate()
    const [cities, setCities] = useState<CityModel[]>([])
    const [selectedFile, setSelectedFile] = useState<Blob | null>(null);
    const [selectedCity, setSelectedCity] = useState("");

    const getAllCities = async () => {
        const response = await axiosPrivate.get("city")
        setCities(response.data)
    }

    const handleFileChange = (event) => {
        setSelectedFile(event.target.files[0]);
    }

    const handleCityChange = (event) => {
        setSelectedCity(event.target.value);
    }

    const submitAppliance = async (event) => {
        event.preventDefault()
        const form = new FormData()
        if (selectedFile) {
            console.log(1)
            form.append('image', selectedFile as Blob);
        }
        if (selectedCity) {
            form.append('cityId', selectedCity);
        }
        console.log(selectedCity)
        console.log(form.get("image"))
        try {
            const response = await axiosPrivate.postForm("user/apply-guide", form)
            console.log(response)
        } catch (err) {
            console.log(err)
        }
        // TODO CEVA LOGICA CU TOAST POATE AR FI COOL
    }

    useEffect(() => {
        getAllCities();
    }, []);

  return (
    <div className="mainContent">
      <Top />
        <div className="bottomApplyTourist flex">
            <h3>Apply for guide now!</h3>
            <input
                type="file"
                className="file-input file-input-bordered w-full max-w-xs"
                onChange={handleFileChange}
            />
            <select className="select select-bordered w-full max-w-xs" onChange={handleCityChange}>
                <option disabled selected>Pick origin City</option>
                {cities.map((elem) => <option key={elem.id} value={elem.id}>{elem.cityName}, {elem.countryName}</option>)}
            </select>
            <button className="btn" onClick={submitAppliance}>Apply</button>
        </div>
    </div>
  );
};

export default Body;
