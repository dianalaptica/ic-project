import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { UserLogin } from "../Models/UserLogin";
import { UserRegister } from "../Models/UserRegister";

const api = "https://localhost:7093/api/auth/";

export const loginApi = async (email: string, password: string) => {
  try {
    const data = await axios.post<UserLogin>(api + "login", {
      email: email,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerApi = async (
  firstName: string,
  lastName: string,
  email: string,
  phoneNumber: string,
  password: string
) => {
  try {
    const data = await axios.post<UserRegister>(api + "register", {
      firstName: firstName,
      lastName: lastName,
      email: email,
      phoneNumber: phoneNumber,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};
