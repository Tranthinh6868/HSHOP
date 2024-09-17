using System.Data;
using Dapper;

namespace WebApp.Models;

public class VnPaymentRespository : BaseRepository{

    public VnPaymentRespository (IDbConnection connection) : base (connection){

    }

    public int Add(VnPaymentResponse obj){

        string sql ="INSERT INTO VnPayment(	Amount, BIGINT, BankCode,BankTranNo ,CardType, OrderInfo ,PayDate, ResponseCode,TmnCode, TransactionNo, TransaactionStatus,TxnRef)VALUES( @Amount @BIGINT, @BankCode, @BankTranNo ,@CardType, @OrderInfo ,@PayDate, @ResponseCode,@TmnCode, @TransactionNo, @TransaactionStatus,@TxnRef)" ;
        return connection.Execute(sql, obj);
    }

}