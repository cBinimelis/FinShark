import React from "react";

interface Props {
  companyName: string;
  ticker: string;
  price: number;
}

const Card: React.FC<Props> = ({
  companyName,
  ticker,
  price,
}: Props): JSX.Element => {
  return (
    <div className="card" style={{ width: "30vw" }}>
      <img
        src="https://images.unsplash.com/photo-1634908714925-ff95d2b6e54e?q=80&w=1970&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="Image"
        style={{ width: "20vw" }}
      />
      <div className="details">
        <h2>{companyName}</h2>
        <p>${price}</p>
        <p>${ticker}</p>
      </div>
      <p className="info">
        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aspernatur
        porro facere, error expedita doloremque ipsum alias corrupti saepe
        asperiores, iusto, voluptas quos incidunt deleniti ea! Soluta voluptas
        fuga totam illo.
      </p>
    </div>
  );
};

export default Card;
