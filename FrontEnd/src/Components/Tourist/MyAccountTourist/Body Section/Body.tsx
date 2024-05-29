import "./Body.css";
import Top from "./Top Section/Top";

// request ca sa iei datele userului pt a le edita

const Body = () => {
  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">my acc</div>
      <label className="input input-bordered flex items-center gap-2">
        Name
        <input type="text" className="grow" placeholder="Daisy" />
      </label>
    </div>
  );
};

export default Body;
