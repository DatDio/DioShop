using DioShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Infrastructure.Configurations.Entities
{
	public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{


		}
	}
}
