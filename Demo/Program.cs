﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EthereumRpc;
using EthereumRpc.Ethereum;
using EthereumRpc.RpcObjects;
using EthereumRpc.Service;

using HashLib;


namespace Demo
{
    class Program
    {


        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }



        static void Main(string[] args)
        {
         


            var liveConnection = new ConnectionOptions()
            {
                Port = "8545",
                Url = "http://localhost"
            };
            Console.WriteLine("Hello World");
            var privateConnection = new ConnectionOptions()
            {
                Port = "8545",
                Url = "http://localhost"
            };

            var ethereumService = new Web3Mobile(privateConnection);

            var exampleTxHash = "0x79b636e7a28e74b6d1db3be815e2658c759dd3213605ca7916b3a19402d0ba42";
            var exampleBlockHash = "0xca3130089dca52a645b1545a0f04930257dea601b54011aececd616d04fec12f";
            var exampleBlockNumber = 514064;
            var exampleAddress = "0x63a9975ba31b0b9626b34300f7f627147df1f526";


            var privateAddress1 = "0xb884FF4b46C7d0cCb68d280EF39B3cBE8Fd47590";
            var privateAddress2 = "0x7b953144Da42fe99d15411ae9e945C877b1B839c";

            Console.WriteLine(ethereumService.GetBalance("0xb884FF4b46C7d0cCb68d280EF39B3cBE8Fd47590", BlockTag.Latest));


            var t = new Transaction() {From = privateAddress1, To = privateAddress2, Value = "0x9184e72a" };
            Console.WriteLine(ethereumService.SendTransaction(t));
            Console.WriteLine(ethereumService.SendTransaction(privateAddress1, privateAddress2, -1, null,-1,350000)); //return nothing

            Console.WriteLine(ethereumService.GetWeb3ClientVersion());
            Console.WriteLine(ethereumService.GetWeb3Sha3("Hello world"));
            Console.WriteLine(ethereumService.GetNetVersion());
            Console.WriteLine(ethereumService.GetNetListening());
            //Console.WriteLine(ethereumService.GetNetPeerCount());
            Console.WriteLine(ethereumService.GetProtocolVersion());
            Console.WriteLine(ethereumService.GetSyncing());
            Console.WriteLine(ethereumService.GetCoinbase().Value);
            Console.WriteLine(ethereumService.GetMining());
            Console.WriteLine(ethereumService.GetHashrate());
            Console.WriteLine(ethereumService.GetGasPrice());
            ethereumService.GetAccounts().ToList().ForEach(i => Console.Write(@"[{0}] ", i));

            var accounts = ethereumService.GetAccounts();

            foreach (var account in accounts)
            {
                var balance = ethereumService.GetBalance(account, BlockTag.Latest);
                Console.WriteLine("account :" + EtherCurrencyConverter.Convert(balance));

                var sign = ethereumService.Sign(account, "Mobile Gaming");

                Console.WriteLine("sign : " + sign);
            }

            Console.WriteLine(ethereumService.GetBlockNumber());
            Console.WriteLine(ethereumService.GetBalance(exampleAddress, BlockTag.Latest));
            Console.WriteLine(ethereumService.GetStorageAt(exampleAddress, 100, BlockTag.Latest));
            Console.WriteLine(ethereumService.GetTransactionCount(exampleAddress, BlockTag.Latest));
            Console.WriteLine(ethereumService.GetBlockTransactionCountByHash(exampleBlockHash));
            Console.WriteLine(ethereumService.GetBlockTransactionCountByNumber(BlockTag.Latest));
            Console.WriteLine(ethereumService.GetUncleCountByBlockHash(exampleBlockHash));
            Console.WriteLine(ethereumService.GetUncleCountByBlockNumber(10));
            Console.WriteLine(ethereumService.GetCode(exampleAddress, BlockTag.Latest));

            Console.WriteLine(ethereumService.Sign(exampleAddress, "School bus")); // return nothing
            Console.WriteLine("Send Transaction");
            Console.WriteLine(ethereumService.SendTransaction("0xb884FF4b46C7d0cCb68d280EF39B3cBE8Fd47590", "0x7b953144Da42fe99d15411ae9e945C877b1B839c", 30400,"0xd46e8dd67c5d32be8d46e8dd67c5d32be8058bb8eb970870f072445675058bb8eb970870f072445675")); //return nothing
            Console.WriteLine("Send Raw Transaction");
            Console.WriteLine(ethereumService.SendRawTransaction("0xd46e8dd67c5d32be8d46e8dd67c5d32be8058bb8eb970870f072445675058bb8eb970870f072445675")); // returns nothing
            //Console.WriteLine(ethereumService.Call("0xb60e8dd61c5d32be8058bb8eb970870f07233155", "0xd46e8dd67c5d32be8058bb8eb970870f072445675", 30400, "0xd46e8dd67c5d32be8d46e8dd67c5d32be8058bb8eb970870f072445675058bb8eb970870f072445675")); //return nothing

            //Console.WriteLine(ethereumService.EstimateGas()); // not yet implemented
            //Console.WriteLine(ethereumService.GetBlockByHash(exampleBlockHash, true)); // works but need to fix partial value return (fullblock:false)
            //Console.WriteLine(ethereumService.GetBlockByNumber(exampleBlockNumber, BlockTag.Quantity,true));
            //Console.WriteLine(ethereumService.GetTransactionByHash(exampleTxHash));

            //Console.WriteLine(ethereumService.GetTransactionByBlockHashAndIndex(exampleBlockHash,0));
            //Console.WriteLine(ethereumService.GetTransactionByBlockNumberAndIndex(exampleBlockNumber, 0));
            //Console.WriteLine(ethereumService.GetTransactionReceipt(exampleTxHash));
            //Console.WriteLine(ethereumService.GetUncleByBlockHashAndIndex(exampleBlockHash,0)); // doesnt seem to return the uncle
            //Console.WriteLine(ethereumService.GetUncleByBlockNumberAndIndex(exampleBlockNumber, 0)); // doesnt seem to return the uncle

            //ethereumService.GetCompilers().ToList().ForEach(i => Console.Write(@"[{0}] ", i));

            //var contract = @"contract test { function multiply(uint a) returns(uint d) {   return a * 7;   } }";


            //Console.WriteLine(ethereumService.CompileSolidity(contract)); //not working
            //Console.WriteLine(ethereumService.CompileLLL()); //not yet implemented
            //Console.WriteLine(ethereumService.CompileSerpent()); //not yet implemented

            //var filter = ethereumService.NewFilter(exampleAddress);
            //Console.WriteLine(filter);
            //var blockFilter = ethereumService.NewBlockFilter();
            //Console.WriteLine(blockFilter);
            //var newPendingTransactionFilter = ethereumService.NewPendingTransactionFilter();
            //Console.WriteLine(newPendingTransactionFilter);

            //ethereumService.GetFilterChanges(blockFilter).ToList().ForEach(i => Console.Write(@"[{0}] ", i)); // return the right objects
            //ethereumService.GetFilterLogs(blockFilter).ToList().ForEach(i => Console.Write(@"[{0}] ", i)); // return the right objects

            //var uninstallFilter = ethereumService.UninstallFilter(filter);
            //Console.WriteLine(uninstallFilter);

            // Console.WriteLine(ethereumService.GetLogs(new Log())); // not working

            //Console.WriteLine(ethereumService.GetWork());
            //Console.WriteLine(ethereumService.SubmitWork("0x0000000000000001","0x1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef", "0xD1FE5700000000000000000000000000D1FE5700000000000000000000000000")); // errors probably due to parameters passed
            //Console.WriteLine(ethereumService.SubmitHashrate("0x0000000000000000000000000000000000000000000000000000000000500000","0x59daa26581d0acd1fce254fb7e85952f4c09d0915afd33d3886cd914bc7d283c")); // return null. probably bad parameters

            //Console.WriteLine(ethereumService.ShhVersion());

            //Console.WriteLine(ethereumService.ShhPost(null, null, null, null, null, null));

            Console.ReadLine();
        }
    }
}
