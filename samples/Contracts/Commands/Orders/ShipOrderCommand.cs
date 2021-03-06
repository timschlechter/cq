﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Contracts.Commands.Orders
{
    [DataContract]
    [Description("Ships the order with the given id")]
    public class ShipOrderCommand
    {
        [Required]
        [DataMember(Name = "orderId")]
        public Guid OrderId { get; set; }
    }
}