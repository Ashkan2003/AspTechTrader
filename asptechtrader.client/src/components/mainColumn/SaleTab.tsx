/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
import { TextField, Button, Typography } from "@mui/material";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import toast from "react-hot-toast";
import { SymbolType,  UserType} from "../../types/types";
import { useSaleSymbol } from "../../features/reactQueryUserSymbolProperty/useSaleSymbol";

interface Props {
    currentSymbol: SymbolType;
    currentUser: UserType;
    currentBoughtSymbolQuantity: number;
    currentBoughtSymbolId: string;
    priceInputValue: number;
    volumeInputValue: number;
    todayDate: string;
    setPriceInputValue: React.Dispatch<React.SetStateAction<number>>;
    setVolumeInputValue: React.Dispatch<React.SetStateAction<number>>;
    handleSetUserBoughtSymbolCountToVulomeInput: any;
    handleClose: any;
}

const SaleTab = ({
    currentSymbol,
    currentUser,
    currentBoughtSymbolQuantity,
    currentBoughtSymbolId,
    priceInputValue,
    volumeInputValue,
    todayDate,
    setPriceInputValue,
    setVolumeInputValue,
    handleClose,
    handleSetUserBoughtSymbolCountToVulomeInput,
}: Props) => {
  // calc the
  const finalOrderPrice = priceInputValue * volumeInputValue;

    const { saleSymbolMutation } = useSaleSymbol()

    // get the userProperty from 
    const userCurrentProperty = currentUser.userProperty;

  // calc the user new property // add the finalOrderPrice to userCurrentProperty
  const userNewProperty = userCurrentProperty! + finalOrderPrice;

  //when the user clicks on the sale btn run this...
  const handleFinalBuy = () => {
    // if the state was notAllowed so dont let the user buy or sale it
    if (currentSymbol.state == "NOTALLOWED") {
      toast.error("وضعیت نماد درحالت ممنوع معامله قرار دارد.");
      return null;
    }

      // we want to prevent the user from saling the symbols that he dont have any count of it// so if the userCurrentBoughtSymbol was null render nothing
      if (currentBoughtSymbolQuantity == 0) {
      toast.error("شما در این نماد دارایی ندارید");
      return null;
    }

      // the user canot sale more that its bought-symbol-count
      if (volumeInputValue > currentBoughtSymbolQuantity) {
      toast.error("شما نمی توانید بیشتر از دارایی خود حجم به فروش برسانید");
      return null;
    }

    // this condition is for checking the renge of price that user entried
    if (
      priceInputValue > currentSymbol.lastPrice ||
      priceInputValue < currentSymbol.theLeast
    ) {
      toast.error("لطفا قیمت پیشنهادی خود را بین رنج قیمتی وارد کنید.");
      return null;
    }

    // this condition is for checking the renge of volume that user entried
    if (volumeInputValue > 100 || volumeInputValue < 10) {
      toast.error("لطفا حجم پیشنهادی خود را بین رنج حجمی وارد کنید.");
      return null;
    }

      // sale
      saleSymbolMutation({
          symbolId: currentBoughtSymbolId,
          userId : currentUser.userId,
          symbolSalePrice : priceInputValue,
          symbolSaleQuantity : volumeInputValue,
      })
    
    
    // close the model
    handleClose();

    // set to 0
    setPriceInputValue(0);
    setVolumeInputValue(0);
  };

  return (
    <div className="flex-col justify-between">
      <div className="grid grid-cols-2 gap-2">
        {/* user property */}
        <TextField
          focused
          color="info"
          id="filled-read-only-input"
          label="مانده"
          value={userCurrentProperty}
          InputProps={{
            readOnly: true,
          }}
          variant="filled"
        />
        <TextField
          focused
          color="info"
          id="filled-read-only-input"
          label="دارایی سپرده گزاری"
                  value={currentBoughtSymbolQuantity}
          onClick={(event: any) =>
            handleSetUserBoughtSymbolCountToVulomeInput(
              event,
                currentBoughtSymbolQuantity
            )
          }
          InputProps={{
            readOnly: true,
          }}
          variant="filled"
        />
        <TextField
          focused
          color="info"
          id="filled-read-only-input"
          label="اعتبار تا تاریخ:"
          defaultValue={todayDate}
          InputProps={{
            readOnly: true,
          }}
          variant="filled"
        />
      </div>
      <div className="flex items-center  mt-11">
        <TextField
          value={finalOrderPrice}
          color="warning"
          focused
          label="ارزش ناخالص سفارش"
          id="outlined-size-small"
          size="small"
          InputProps={{
            readOnly: true,
          }}
        />
        <Button
          sx={{
            width: { xs: "90px", md: "100px" },
            height: "41px",
            ml: "10px",
          }}
          size="large"
          variant="outlined"
          color="error"
          startIcon={<ShoppingCartIcon className="text-red-600" />}
          onClick={handleFinalBuy}
        >
          <Typography className="text-red-600">فروش</Typography>
        </Button>
      </div>
    </div>
  );
};

export default SaleTab;
