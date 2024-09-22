/* eslint-disable @typescript-eslint/no-unused-vars */
import { TextField, Button, Typography } from "@mui/material";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import toast from "react-hot-toast";
import { useBuySymbol } from "../../features/reactQueryUserSymbolProperty/useBuySymbol";
import { SymbolType,  UserType } from "../../types/types";
import { useUpdateUserProperty } from "../../features/reactQueryUser/useUpdateUserProperty";

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

const BuyTab = ({
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

    //react-query // update
    const { mutate } = useBuySymbol();
    const { updateUserPropertyMutation} = useUpdateUserProperty()
    //
    const userCurrentProperty = currentUser.userProperty;

    //when the user clicks on the buy btn run this...
    const handleFinalBuy = () => {
    // if the state was notAllowed so dont let the user buy or sale it
    if (currentSymbol.state == "NOTALLOWED") {
      toast.error("وضعیت نماد درحالت ممنوع معامله قرار دارد.");
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

    // this condition is for when the user-property if not sufficent to buy the current symbol
    if (userCurrentProperty! < finalOrderPrice) {
      toast.error("موجودی حساب شما کافی نمی باشد.");
      return null;
    }

    // calc the user new property
    const userNewProperty = userCurrentProperty! - finalOrderPrice;

    // update the bought-symbol
    mutate(
        {
            symbolPrice: priceInputValue,
            symbolQuantity: volumeInputValue,
            symbolId: currentBoughtSymbolId,
            userId: currentUser.userId,
      },
      {
        onSuccess: () => {
              // when the update-mutate of the buying a symbol was successfull update the userProperty
              updateUserPropertyMutation(
                  {
                      userId: currentUser.userId,
                      userProperty: userNewProperty
                  })
        },
      }
    );

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
          color="warning"
          startIcon={<ShoppingCartIcon className="text-green-600" />}
          onClick={() => handleFinalBuy()}
        >
          <Typography className="text-green-600">خرید</Typography>
        </Button>
      </div>
    </div>
  );
};

export default BuyTab;
