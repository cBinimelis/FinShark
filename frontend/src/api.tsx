import axios from "axios";
import { CompanySearch } from "./company";
interface SearchResponse {
  data: CompanySearch[];
}

const api = "Uf6sbwv3yb32izPImqEDLw9L3KCSp20d";

export const searchCompanies = async (query: string) => {
  try {
    const data = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${api}`
    );
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error message: ", error.message);
      return error.message;
    } else {
      console.log("unexpected error: ", error);
      return "An unexpected error has occured";
    }
  }
};
