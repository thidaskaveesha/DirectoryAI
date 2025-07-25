import os
from dotenv import load_dotenv

load_dotenv()

GROQ_API_KEY = os.getenv("GROQ_API_KEY")
MODEL_NAME = "llama3-8b-8192"
EMBEDDING_MODEL = "sentence-transformers/all-mpnet-base-v2"
