import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { ApplianceModel } from "../../../Models/ApplianceModel.ts";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [appliances, setAppliances] = useState<ApplianceModel[]>();

  const getAllAppliances = async () => {
    const response = await axiosPrivate.get("user/apply-guide");
    if (response.status === 404) {
      setAppliances([]);
    } else {
      setAppliances(response.data);
    }
  };

  const acceptGuide = async (id: number) => {
    const response = await axiosPrivate.patch(`user/accept-guide/${id}`);
    getAllAppliances();
  };

  useEffect(() => {
    getAllAppliances();
  }, []);
  // TODO MAKE SIMILAR LOGIC LIKE THE NOTIFICATION ONE
  // DO NOT IMPLEMENT METHOD TO DELETE OR TO UNACCEPT A USER
  // LOGIC IN THE BACKEND MIGHT BE AFFECTED
  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">see applications here</div>
      {appliances?.map((elem) => (
        <div key={elem.userId}>
          <p>{elem.userId}</p>
          <p>{elem.countryName}</p>
          <p>{elem.cityName}</p>
          <button onClick={() => acceptGuide(elem.userId)}>ACCEPT</button>
          <p>-------------</p>
        </div>
      ))}
    </div>
  );
};

export default Body;
