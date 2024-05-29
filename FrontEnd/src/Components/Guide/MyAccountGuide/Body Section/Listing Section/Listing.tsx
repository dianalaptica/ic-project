import "./Listing.css";
import paris from "../../../../../LoginAssets/paris.png";
import london from "../../../../../LoginAssets/london.png";
import useAxiosPrivate from "../../../../../Hooks/useAxiosPrivate.ts";

const Listing = () => {
  const axiosPrivate = useAxiosPrivate();

  const callApi = async (cityId: number) => {
    try {
      const response = await axiosPrivate.get(`/trips?cityId=${cityId}`);
      console.log(response.data); // TODO: from here somehow render the components
      // TODO: the endpoint return Not Found if there are no trips and/or the city does not exist
      // London and Berlin is in this scenario
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className="listingSection">
      <div className="heading flex">
        <h1>Create trips in your most frequented locations</h1>
      </div>

      <div className="secContainer flex">
        <div
          className="singleItem"
          onClick={async () => {
            await callApi(14);
          }}
        >
          <img src={paris} alt="Eiffel Tower" />
          <h3>Paris, FR</h3>
        </div>

        <div
          className="singleItem"
          onClick={async () => {
            await callApi(17);
          }}
        >
          <img src={london} alt="Big Ben" />
          <h3>London, UK</h3>
        </div>
      </div>
    </div>
  );
};

export default Listing;
