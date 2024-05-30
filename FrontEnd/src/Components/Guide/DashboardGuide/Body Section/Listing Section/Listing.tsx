import "./Listing.css";
import cluj from "../../../../../LoginAssets/cluj.png";
import timisoara from "../../../../../LoginAssets/timisoara.png";
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
          <img src={timisoara} alt="Eiffel Tower" />
          <h3>Timisoara, RO</h3>
        </div>

        <div
          className="singleItem"
          onClick={async () => {
            await callApi(17);
          }}
        >
          <img src={cluj} alt="Catedrala" />
          <h3>Cluj-Napoca, RO</h3>
        </div>
      </div>
    </div>
  );
};

export default Listing;
