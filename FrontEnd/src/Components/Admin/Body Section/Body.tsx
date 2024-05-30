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
      console.log(appliances);
    }
  };

  const acceptGuide = async (id: number) => {
    const response = await axiosPrivate.patch(`user/accept-guide/${id}`);
    getAllAppliances();
  };

  useEffect(() => {
    getAllAppliances();
  }, []);

  function base64ToBlob(base64String: string, contentType: string) {
    contentType = contentType || "";
    const byteCharacters = atob(base64String);
    const byteNumbers = new Array(byteCharacters.length);

    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: contentType });
  }

  const createImageSrc = (imageFile) => {
    return URL.createObjectURL(base64ToBlob(imageFile, "image/png"));
  };

  // TODO MAKE SIMILAR LOGIC LIKE THE NOTIFICATION ONE
  // DO NOT IMPLEMENT METHOD TO DELETE OR TO UNACCEPT A USER
  // LOGIC IN THE BACKEND MIGHT BE AFFECTED
  return (
    <div className="mainContent">
      <Top />
      <div className="bottomAdmin flex">
        <h2>Applications</h2>
        {appliances?.map((elem) => (
          <div
            key={elem.userId}
            className="card card-side bg-base-100 shadow-xl"
          >
            <figure className="fig">
              <img src={createImageSrc(elem.identityCard)} alt="ID Card" />
            </figure>
            <div className="card-body">
              <h2 className="card-title">Application {elem.userId}</h2>
              <p></p>
              <p>Country: {elem.countryName}</p>
              <p>City: {elem.cityName}</p>
              <p></p>
              <p></p>
              <p></p>
              <div className="card-actions justify-end">
                <button
                  className="btn"
                  onClick={() => acceptGuide(elem.userId)}
                >
                  Accept
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Body;
