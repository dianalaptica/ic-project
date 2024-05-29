import "./Body.css";
import Top from "./Top Section/Top";

const Body = () => {
  return (
    <div className="mainContent">
      <Top />
      <div className="bottomApplyTourist flex">
        <h3>Apply for guide now!</h3>
        <input
          type="file"
          className="file-input file-input-bordered w-full max-w-xs"
        />
        <button className="btn">Apply</button>
      </div>
    </div>
  );
};

export default Body;
